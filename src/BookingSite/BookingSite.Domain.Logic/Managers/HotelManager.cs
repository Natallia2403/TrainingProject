using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingSite.Data;
using BookingSite.Data.Models;
using BookingSite.Domain.Logic.Interfaces;
using BookingSite.Domain.DTO;
using AutoMapper;

namespace BookingSite.Domain.Logic.Managers
{
    public class HotelManager : IHotelManager
    {
        DataContext _dataContext;
        IMapper _mapper;

        public HotelManager(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<HotelDTO>> GetAllAsync()
        {
            var models = _dataContext.Hotels.AsNoTracking().Include(u => u.Country).Include(u => u.Rooms);

            var dto = await _mapper.ProjectTo<HotelDTO>(models).ToListAsync();

            return dto;
        }

        public async Task<IEnumerable<HotelDTO>> GetByUserIdAsync(string userId)
        {
            var models = _dataContext.Hotels.AsNoTracking()
                .Include(u => u.Country)
                .Include(u => u.Rooms)
                .Where(u => u.UserId == userId);

            var dto = await _mapper.ProjectTo<HotelDTO>(models).ToListAsync();

            return dto;
        }

        public async Task<HotelDTO> GetByIdAsync(int? id)
        {
            id = id ?? throw new ArgumentNullException(nameof(id));

            var model = await _dataContext.Hotels.AsNoTracking().Include(u => u.Country).Include(u => u.Rooms).FirstOrDefaultAsync(h => h.Id == id);
            if (model != null)
            {
                var dto = _mapper.Map<HotelDTO>(model);
                return dto;
            }
            throw new Exception("Отель не найден");
        }

        public async Task AddAsync(HotelDTO dto)
        {
            dto = dto ?? throw new ArgumentNullException(nameof(dto));

            var model = _mapper.Map<Hotel>(dto);

            _dataContext.Hotels.Add(model);

            await _dataContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(HotelDTO dto)
        {
            dto = dto ?? throw new ArgumentNullException(nameof(dto));

            var model = _mapper.Map<Hotel>(dto);

            _dataContext.Hotels.Update(model);

            await _dataContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int? id)
        {
            id = id ?? throw new ArgumentNullException(nameof(id));

            var dto = await GetByIdAsync(id);

            if (dto.Rooms != null && dto.Rooms.Count > 0)
            {
                throw new Exception("Пожалуйста, сначала удалите комнаты");
            }

            await DeleteAsync(dto);
        }

        public async Task DeleteAsync(HotelDTO dto)
        {
            dto = dto ?? throw new ArgumentNullException(nameof(dto));

            var model = _mapper.Map<Hotel>(dto);

            _dataContext.Hotels.Remove(model);

            await _dataContext.SaveChangesAsync();
        }
    }
}
