﻿using ByteShop.Domain.Emuns;

namespace ByteShop.Application.DTOs;
public class CategoryDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public CategoryLevel CategoryLevel { get; set; }
    public int? ParentCategoryId { get; set; }
}
