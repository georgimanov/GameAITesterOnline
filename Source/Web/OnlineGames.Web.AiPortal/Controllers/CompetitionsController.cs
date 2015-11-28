﻿// <copyright file="CompetitionsController.cs" company="Nikolay Kostov (Nikolay.IT)">
// Copyright (c) Nikolay Kostov (Nikolay.IT). All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace OnlineGames.Web.AiPortal.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using OnlineGames.Common;
    using OnlineGames.Data.Common;
    using OnlineGames.Data.Models;
    using OnlineGames.Services.AiPortal.Battles;
    using OnlineGames.Web.AiPortal.ViewModels.Competitions;

    public class CompetitionsController : BaseController
    {
        private readonly IDbRepository<Competition> competitionsRepository;

        private readonly IDbRepository<Team> teamsRepository;

        private readonly IDbRepository<Battle> battlesRepository;

        private readonly IBattlesGenerator battlesGenerator;

        public CompetitionsController(
            IDbRepository<Competition> competitionsRepository,
            IDbRepository<Team> teamsRepository,
            IDbRepository<Battle> battlesRepository,
            IBattlesGenerator battlesGenerator)
        {
            this.competitionsRepository = competitionsRepository;
            this.teamsRepository = teamsRepository;
            this.battlesRepository = battlesRepository;
            this.battlesGenerator = battlesGenerator;
        }

        [HttpGet]
        public ActionResult Info(int id)
        {
            var activeCompetitions =
                this.competitionsRepository.All()
                    .Where(x => x.IsActive && x.Id == id)
                    .ProjectTo<CompetitionViewModel>()
                    .FirstOrDefault();
            return this.View(activeCompetitions);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public ActionResult CreateBattles(int id)
        {
            var newBattles = this.battlesGenerator.GenerateBattles(this.teamsRepository, this.battlesRepository, id);
            this.TempData["Info"] = $"{newBattles} new battles created successfully!";
            return this.RedirectToAction(nameof(this.Info), new { id });
        }
    }
}
