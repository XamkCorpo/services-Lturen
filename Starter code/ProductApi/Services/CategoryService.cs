using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Logging;
using ProductApi.Common;
using ProductApi.Mappings;
using ProductApi.Models;
using ProductApi.Models.Dtos;
using ProductApi.Repositories;
using System.Collections.Generic;


namespace ProductApi.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ILogger<CategoryService> _logger;
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository, ILogger<CategoryService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Result<List<CategoryResponse>>> GetAllAsync()
        {
            try
            {
                List<Category> categories = await _repository.GetAllAsync();
                return Result.Success(categories.Select(c => c.ToResponse()).ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching categories");
                return Result.Failure<List<CategoryResponse>>("Error fetching categories");
            }
            
        }

        public async Task<Result<CategoryResponse>> GetByIdAsync(int id)
        {
            try
            {
                Category? category = await _repository.GetByIdAsync(id);
                if (category == null)
                    return Result.Failure<CategoryResponse>($"Kategoriaa {id} ei löytynyt");

                return Result.Success(category.ToResponse());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching category with id {CategoryId}", id);
                return Result.Failure<CategoryResponse>($"Error fetching category with id {id}");
            }
            
        }

        public async Task<Result<CategoryResponse>> CreateAsync(CreateCategoryRequest request)
        {
            try
            {
                Category category = request.ToEntity();
                Category created = await _repository.AddAsync(category);
                return Result.Success(created.ToResponse());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating category");
                return Result.Failure<CategoryResponse>("Error creating category");
            }
        }

        public async Task<Result<CategoryResponse>> UpdateAsync(int id, UpdateCategoryRequest request)
        {
            try
            {
                Category? existing = await _repository.GetByIdAsync(id);

                if (existing == null)
                    return Result.Failure<CategoryResponse>($"Kategoriaa {id} ei löytynyt");

                request.UpdateEntity(existing);
                await _repository.UpdateAsync(existing);
                return Result.Success(existing.ToResponse());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating category with id {CategoryId}", id);
                return Result.Failure<CategoryResponse>($"Error updating category with id {id}");
            }
        }

        public async Task<Result<bool>> DeleteAsync(int id)
        {
            try
            {
                bool deleted = await _repository.DeleteAsync(id);
                return Result.Success(deleted);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting category with id {CategoryId}", id);
                return Result.Failure<bool>($"Error deleting category with id {id}");
            }
        }
    
    }
}

