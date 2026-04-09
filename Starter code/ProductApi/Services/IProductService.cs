using ProductApi.Models.Dtos;

namespace ProductApi.Services
{
    public interface IProductService
    {
        Task<List<ProductResponse>> GetAllAsync(); //Asyncroninen lista tuotteista dto muodossa.
        Task<ProductResponse?> GetByIdAsync(int id); //Asyncroninen yksittäinen tuote dto muodossa.
        Task<ProductResponse> CreateAsync(CreateProductRequest request); //Asyncroninen tuotteen luonti.
        Task<ProductResponse?> UpdateAsync(int id, UpdateProductRequest request); //Tuotteen muokkaaminen, dto.
        Task<bool> DeleteAsync(int id); //Asyncroninen bool, joka kertoo onnistuiko poisto vaiko ei.
    }
}
