﻿using System;
using Final_Project.Models;
using Final_Project.Areas.Admin.ViewModels.Episode;
using Final_Project.Data;
using Microsoft.EntityFrameworkCore;

namespace Final_Project.Services.Interfaces
{
	public interface IEpisodeService
	{
        Task<List<Episode>> GetAllEpisodes();
        Task<Episode> GetByIdAsync(int? id);
        Task<List<Episode>> GetAllByIdAsync(int? id);
        EpisodeDetailVM GetMappedData(Episode episode);
        Task DeleteAsync(int id);
        Task CreateAsync(EpisodeCreateVM model);
        Task EditAsync(int episodeId, EpisodeEditVM model);
        Task<Episode> GetByIdWithAllIncludesAsync(int? id);

        List<EpisodeVM> GetMappedDatas(List<Episode> episodes);
        Task<List<Episode>> GetPaginatedDatasAsync(int page, int take);
        Task<int> GetCountAsync();
    }
}

