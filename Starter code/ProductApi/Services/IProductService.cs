using ProductApi.Models.Dtos;
using ProductApi.Common;

namespace ProductApi.Services
{
    public interface IProductService
    {
        Task<Result<List<ProductResponse>>> GetAllAsync(); //Asyncroninen lista tuotteista dto muodossa. //Kommentit eivät enää ole valideja muokkauksen jälkeen.
        Task<Result<ProductResponse?>> GetByIdAsync(int id); //Asyncroninen yksittäinen tuote dto muodossa.
        Task<Result<ProductResponse>> CreateAsync(CreateProductRequest request); //Asyncroninen tuotteen luonti.
        Task<Result<ProductResponse?>> UpdateAsync(int id, UpdateProductRequest request); //Tuotteen muokkaaminen, dto.
        Task<Result> DeleteAsync(int id); //Asyncroninen bool, joka kertoo onnistuiko poisto vaiko ei.
    }
}
