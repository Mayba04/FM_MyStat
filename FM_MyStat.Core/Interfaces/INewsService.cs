using FM_MyStat.Core.DTOs.GrouopsDTO;
using FM_MyStat.Core.DTOs.NewsDTO;
using FM_MyStat.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.Interfaces
{
    public interface INewsService
    {
        Task<List<NewsDTO>> GetAll();
        Task<NewsDTO?> Get(int id);
        Task Create(CreateNewsDTO model);
        Task Update(EditNewsDTO model);
        Task Delete(int id);
        Task<EditNewsDTO?> GetEdit(int id);
    }
}
