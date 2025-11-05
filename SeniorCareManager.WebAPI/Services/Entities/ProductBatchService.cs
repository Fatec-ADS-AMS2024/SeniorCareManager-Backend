using AutoMapper;
using SeniorCareManager.WebAPI.Data.Interfaces;
using SeniorCareManager.WebAPI.Objects.Contracts.Exceptions.Exceptions;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Objects.Models;
using SeniorCareManager.WebAPI.Services.Interfaces;

namespace SeniorCareManager.WebAPI.Services.Entities
{
    public class ProductBatchService : GenericService<ProductBatch, ProductBatchDTO>, IProductBatchService
    {
        private readonly IProductBatchRepository _repository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductBatchService(IProductBatchRepository repository, IProductRepository productRepository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductBatchDTO> GetById(long id)
        {
            var entity = await _repository.GetById(id);
            if (entity == null)
            {
                throw new ExceptionNotFound($"Lote com id {id} não encontrado.");
            }
            return _mapper.Map<ProductBatchDTO>(entity);
        }

        public async Task<IEnumerable<ProductBatchDTO>> GetByProduct(long productId)
        {
            var list = await _repository.GetByProduct(productId);
            return _mapper.Map<IEnumerable<ProductBatchDTO>>(list);
        }

        public async Task<IEnumerable<ProductBatchDTO>> GetExpiringBatches(int daysAhead = 30)
        {
            var cutoffDate = DateTime.UtcNow.AddDays(daysAhead);
            var expiringBatches = await _repository.GetExpiringBatchesAsync(cutoffDate);
            return _mapper.Map<IEnumerable<ProductBatchDTO>>(expiringBatches);
        }

        public async Task<IEnumerable<ProductBatchDTO>> GetExpiredBatches()
        {
            var expiredBatches = await _repository.GetExpiredBatchesAsync();
            return _mapper.Map<IEnumerable<ProductBatchDTO>>(expiredBatches);
        }

        public override async Task<ProductBatchDTO> Create(ProductBatchDTO entityDTO)
        {
            if (entityDTO.CurrentQuantity <= 0)
            {
                throw new ExceptionBadRequest("A quantidade inicial do lote deve ser maior que zero.");
            }

            await ValidateProductBatch(entityDTO);
            return await base.Create(entityDTO);
        }

        public async Task Update(ProductBatchDTO entityDTO, long id)
        {
            var existingEntity = await _repository.GetById(id);
            if (existingEntity == null)
            {
                throw new ExceptionNotFound($"Lote com id: {id} não encontrado para atualização.");
            }

            await ValidateProductBatch(entityDTO, id);

            _mapper.Map(entityDTO, existingEntity);
            await _repository.Update(existingEntity);
        }

        public async Task Remove(long id)
        {
            var entity = await _repository.GetById(id);
            if (entity == null)
            {
                throw new ExceptionNotFound($"Lote com id: {id} não encontrado.");
            }

            if (entity.CurrentQuantity > 0)
            {
                throw new ExceptionBadRequest($"Não é possível remover um lote que ainda possui quantidade em estoque ({entity.CurrentQuantity}).");
            }

            await _repository.Remove(entity);
        }

        private async Task ValidateProductBatch(ProductBatchDTO entityDTO, long? currentBatchId = null)
        {
            var product = await _productRepository.GetById(entityDTO.ProductId);
            if (product == null)
            {
                throw new ExceptionBadRequest($"Produto com ID {entityDTO.ProductId} não encontrado.");
            }

            if (product.ExpirationControlled == Objects.Enums.YesNo.NO && entityDTO.ExpirationDate != default)
            {
                throw new ExceptionBadRequest("Este produto não controla expiração. Data de expiração não deve ser informada.");
            }

            if (product.ExpirationControlled == Objects.Enums.YesNo.YES)
            {
                if (entityDTO.ExpirationDate == default)
                {
                    throw new ExceptionBadRequest("Data de expiração é obrigatória para este produto.");
                }

                if (entityDTO.ExpirationDate <= DateTime.UtcNow)
                {
                    throw new ExceptionBadRequest("Data de expiração deve ser futura.");
                }
            }

            if (await _repository.ExistsByNumberForProductAsync(entityDTO.BatchNumber, entityDTO.ProductId, currentBatchId))
            {
                throw new ExceptionConflict($"Já existe um lote com o número '{entityDTO.BatchNumber}' para este produto.");
            }
        }
    }
}