using AutoMapper;
using SeniorCareManager.WebAPI.Data.Interfaces;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Objects.Models;
using SeniorCareManager.WebAPI.Services.Interfaces;
using System.Reflection.Metadata;

namespace SeniorCareManager.WebAPI.Services.Entities
{
    public class ProductService : GenericService<Product, ProductDTO>, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _productRepository = repository;
            _mapper = mapper;
            
        }
        public async Task<ProductDTO> GetById(long id)
        {
            var entity = await _productRepository.GetById(id);
            return _mapper.Map<ProductDTO>(entity);
        }

        public async Task Update(ProductDTO entityDTO, long id)
        {
            var entity = _mapper.Map<Product>(entityDTO);
            var existingEntity = await _productRepository.GetById(id); // Supondo que sua entidade tenha um campo Id

            if (existingEntity == null)
            {
                throw new KeyNotFoundException($"Produto com id {id} não encontrado!");
            }

            await _productRepository.Update(entity);
        }


        public async Task Remove(long id)
        {
            var entity = await _productRepository.GetById(id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Entidade com id: {id} não encontrado");
            }

            await _productRepository.Remove(entity);
        }
    }
}
