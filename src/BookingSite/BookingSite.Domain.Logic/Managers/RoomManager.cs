using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingSite.Data;
using BookingSite.Data.Models;
using BookingSite.Domain.Logic.Interfaces;
using AutoMapper;
using BookingSite.Domain.DTO;

namespace BookingSite.Domain.Logic.Managers
{
    public class RoomManager : IRoomManager
    {
        DataContext _dataContext;
        IMapper _mapper;

        public RoomManager(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<RoomDTO>> GetAllAsync()
        {
            var models = _dataContext.Rooms.AsNoTracking().Include(u => u.Hotel);

            var dto = await _mapper.ProjectTo<RoomDTO>(models).ToListAsync();

            return dto;
        }

        public async Task<RoomDTO> GetByIdAsync(int? id)
        {
            id = id ?? throw new ArgumentNullException(nameof(id));

            var model = await _dataContext.Rooms.AsNoTracking().Include(u => u.Hotel).FirstOrDefaultAsync(h => h.Id == id);
            if (model != null)
            {
                var dto = _mapper.Map<RoomDTO>(model);
                return dto;
            }
            throw new Exception("Комната не найдена");
        }

        public async Task AddAsync(RoomDTO dto)
        {
            dto = dto ?? throw new ArgumentNullException(nameof(dto));

            var model = _mapper.Map<Room>(dto);

            _dataContext.Rooms.Add(model);

            await _dataContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(RoomDTO dto)
        {
            dto = dto ?? throw new ArgumentNullException(nameof(dto));

            var model = _mapper.Map<Room>(dto);

            _dataContext.Rooms.Update(model);

            await _dataContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int? id)
        {
            id = id ?? throw new ArgumentNullException(nameof(id));

            var dto = await GetByIdAsync(id);

            await DeleteAsync(dto);
        }

        public async Task DeleteAsync(RoomDTO dto)
        {
            dto = dto ?? throw new ArgumentNullException(nameof(dto));

            var model = _mapper.Map<Room>(dto);

            _dataContext.Rooms.Remove(model);

            await _dataContext.SaveChangesAsync();
        }
    }
}
