using AutoMapper;
using SeniorCareManager.WebAPI.Data.Interfaces;
using SeniorCareManager.WebAPI.Data.Repositories;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Objects.Models;
using SeniorCareManager.WebAPI.Services.Interfaces;
using SeniorCareManager.WebAPI.Services.Utils;
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
            var product = await _productRepository.GetById(id);
            if (product is null)
                throw new KeyNotFoundException("Produto com o id " + id + " informado não foi encontrado.");

            return _mapper.Map<ProductDTO>(product);
        }

        public override async Task Create(ProductDTO productDto)
        {
            if (!productDto.CheckName())
                throw new ArgumentException("Nome Inválido.");

            if (await CheckDuplicates(productDto.GenericName))
                throw new InvalidOperationException("Nome duplicados.");

            await base.Create(productDto);
        }

        public async Task Update(ProductDTO productDTO, long id)
        {
            var product = _mapper.Map<Product>(productDTO);
            var existingproduct = await _productRepository.GetById(id); // Supondo que sua entidade tenha um campo Id

            if (existingproduct == null)
            {
                throw new KeyNotFoundException($"Produto com id {id} não encontrado!");
            }

            if (!productDTO.CheckName())
                throw new ArgumentException("Nome Inválido.");

            if (await CheckDuplicates(productDTO.GenericName))
                throw new InvalidOperationException("Nome duplicado.");

            await _productRepository.Update(product);
        }


        public async Task Remove(long id)
        {
            var product = await _productRepository.GetById(id);
            if (product == null)
            {
                throw new KeyNotFoundException($"Entidade com id: {id} não encontrado");
            }

            await _productRepository.Remove(product);
        }
        public async Task<bool> CheckDuplicates(string name)
        {
            var positions = await _productRepository.Get();
            return positions.Any(r => StringValidator.CompareString(r.GenericName, name));
        }
    }
}
