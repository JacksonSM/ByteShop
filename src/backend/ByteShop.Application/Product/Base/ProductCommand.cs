﻿using ByteShop.Application.Configuration.Command;

namespace ByteShop.Application.Product.Base;
public class ProductCommand<TResult> : CommandBase<TResult>
{
    public int Id { get; protected set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string SKU { get; set; }
    public decimal Price { get; set; }
    public decimal CostPrice { get; set; }
    public int Stock { get; set; }
    public int Warranty { get; set; }
    public string Brand { get; set; }
    public float Weight { get; set; }
    public float Height { get; set; }
    public float Length { get; set; }
    public float Width { get; set; }
    public int CategoryId { get; set; }

    public void SetId(int id)
    {
        Id = id;
    }
}
