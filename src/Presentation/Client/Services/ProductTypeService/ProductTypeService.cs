﻿namespace BlazorEcommerce.Client.Services.ProductTypeService;

public class ProductTypeService : IProductTypeService
{
    private readonly HttpClient _http;
    private const string ProductTypeBaseURL = "api/producttype/";

    public ProductTypeService(HttpClient http)
    {
        _http = http;
    }

    public List<ProductTypeDto> ProductTypes { get; set; } = new List<ProductTypeDto>();

    public event Action OnChange;

    public async Task AddProductType(ProductTypeDto productType)
    {
        var response = await _http.PostAsJsonAsync($"{ProductTypeBaseURL}", productType);
        var result = (await response.Content
            .ReadFromJsonAsync<IResponse>());

        if (result != null && result.Success)
        {
            var responseCast = (DataResponse<List<ProductTypeDto>>)result;

            ProductTypes = responseCast.Data;

            OnChange.Invoke();
        }
    }

    public ProductTypeDto CreateNewProductType()
    {
        var newProductType = new ProductTypeDto { IsNew = true, Editing = true };

        ProductTypes.Add(newProductType);
        OnChange.Invoke();
        return newProductType;
    }

    public async Task GetProductTypes()
    {
        var result = await _http
            .GetFromJsonAsync<IResponse>($"{ProductTypeBaseURL}");

        if (result != null && result.Success)
        {
            var responseCast = (DataResponse<List<ProductTypeDto>>)result;

            ProductTypes = responseCast.Data;

        }
    }

    public async Task UpdateProductType(ProductTypeDto productType)
    {
        var response = await _http.PutAsJsonAsync($"{ProductTypeBaseURL}", productType);
        var result = (await response.Content
            .ReadFromJsonAsync<IResponse>());

        if (result != null && result.Success)
        {
            var responseCast = (DataResponse<List<ProductTypeDto>>)result;

            ProductTypes = responseCast.Data;

            OnChange.Invoke();
        }
    }
}