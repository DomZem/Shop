﻿using MediatR;

namespace Shop.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public string? Description { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }   

        public int ProductCategoryId { get; set; }  
    }
}
