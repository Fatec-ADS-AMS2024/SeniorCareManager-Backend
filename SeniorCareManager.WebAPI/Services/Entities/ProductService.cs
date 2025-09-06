using AutoMapper;
using SeniorCareManager.WebAPI.Data.Interfaces;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Objects.Models;
using SeniorCareManager.WebAPI.Services.Interfaces;
using SeniorCareManager.WebAPI.Services.Utils;

namespace SeniorCareManager.WebAPI.Services.Entities;

public class ProductService : GenericService<Product, ProductDTO>, IProductService
{
	private readonly IProductRepository _productRepository;
	private readonly IMapper _mapper;

	public ProductService(IProductRepository repository, IMapper mapper) : base(repository, mapper)
	{
		_productRepository = repository;
		_mapper = mapper;
	}

	public override async Task<ProductDTO> GetById(int id)
	{
		var product = await _productRepository.GetById(id);
		if (product is null)
			throw new ArgumentNullException("Produto com o id " + id + " informado não foi encontrado.");

		return _mapper.Map<ProductDTO>(product);
	}

	public override async Task Create(ProductDTO productDto)
	{
		if (productDto is null)
			throw new ArgumentNullException("O Produto não pode ser nulo.");

		await ValidateBusinessRules(productDto);

		if (await CheckDuplicates(productDto))
			throw new InvalidOperationException("Nome duplicado.");

		await base.Create(productDto);
	}

	public override async Task Update(ProductDTO productDto, int id)
	{
		if (productDto is null)
			throw new ArgumentNullException("O Produto não pode ser nulo.");

		await ValidateBusinessRules(productDto);

		if (await CheckDuplicates(productDto))
			throw new InvalidOperationException("Nome duplicado.");

		await base.Update(productDto, id);
	}

	public override async Task Remove(int id)
	{
		var product = await _productRepository.GetById(id);
		if (product is null)
			throw new ArgumentNullException("Produto com o id " + id + " informado não foi encontrado.");

		await base.Remove(id);
	}

	public async Task<bool> CheckDuplicates(ProductDTO dto)
	{
		var products = await _productRepository.Get();
		return products.Any(p =>
			(p.Id != dto.Id) &&
			StringValidator.CompareString(p.GenericName, dto.GenericName));
	}

	private async Task ValidateBusinessRules(ProductDTO dto)
	{
		if (!dto.CheckName())
			throw new ArgumentException("Nome inválido.");

		// Apenas aviso, não impede a operação
		if (dto.CurrentStock < dto.MinimumStock)
		{
			Console.WriteLine(
				$"[AVISO] Estoque atual ({dto.CurrentStock}) está abaixo do mínimo ({dto.MinimumStock}).");
		
		}

		if (dto.UnitPrice < 0)
			throw new InvalidOperationException("Preço unitário não pode ser negativo.");

		if (dto.LastPurchasePrice < 0)
			throw new InvalidOperationException("Preço da última compra não pode ser negativo.");

		if (dto.StockValue < 0)
			throw new InvalidOperationException("Valor do estoque não pode ser negativo.");
	}

}
