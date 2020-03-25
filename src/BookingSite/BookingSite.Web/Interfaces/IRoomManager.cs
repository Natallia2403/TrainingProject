﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingSite.Data.Models;

namespace BookingSite.Web.Interfaces
{
    public interface IRoomManager
    {
        Task AddAsync(Room room);

        IEnumerable<Room> GetAll();

        Room GetById(int? id);

        Task UpdateAsync(Room room);

        Task DeleteAsync(int? id);
    }
}