﻿using System;
using System.Threading.Tasks;

namespace TechMarket.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        ICategoryRepository Categories { get; }
        Task<int> CommitAsync();
    }
}
