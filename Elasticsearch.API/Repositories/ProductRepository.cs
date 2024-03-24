﻿using DevopsTest.Models;
using Elasticsearch.API.DTOs;
using Nest;
using System.Collections.Immutable;

namespace Elasticsearch.API.Repositories
{
    public class ProductRepository
    {
        private readonly ElasticClient _client;
        private const string IndexName = "products";
        public ProductRepository(ElasticClient client)
        {
            _client = client;
        }

        public async Task<ImmutableList<Product>> GetAllAsync()
        {

            var result = await _client.SearchAsync<Product>(s => s.Index(IndexName).Query(q => q.MatchAll()));
            foreach (var hit in result.Hits) hit.Source.Id = hit.Id;
            return result.Documents.ToImmutableList();
        }

        public async Task<Product?> GetByIdAsync(string id)
        {
            var response = await _client.GetAsync<Product>(id, x => x.Index(IndexName));

            if (!response.IsValid)
            {
                return null;

            }
            response.Source.Id = response.Id;
            return response.Source;
        }

        public async Task<Product?> SaveAsync (Product newProduct)
        {
            newProduct.Created = DateTime.Now;
            var response = await _client.IndexAsync(newProduct, x => x.Index(IndexName));

            if (!response.IsValid) return null;

            newProduct.Id = response.Id;

            return newProduct;
        }

        public async Task<bool> UpdateAsync(ProductUpdateDto updateProduct)
        {
            var response = await _client.UpdateAsync<Product, ProductUpdateDto>(updateProduct.Id, x => x.Index(IndexName).Doc(updateProduct));

            if (!response.IsValid) return false;

            return true;
        }

        public async Task<DeleteResponse> DeleteAsync(string id)
        {

            var response = await _client.DeleteAsync<Product>(id, x => x.Index(IndexName));
            return response;
        }
    }
}
