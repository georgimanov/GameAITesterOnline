﻿// <copyright file="GamesSimulatorResult.cs" company="Nikolay Kostov (Nikolay.IT)">
// Copyright (c) Nikolay Kostov (Nikolay.IT). All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace OnlineGames.Workers.BattlesSimulator.GamesSimulator
{
    using System.Collections.Generic;

    public class GamesSimulatorResult
    {
        public GamesSimulatorResult(string comment)
        {
            this.BattleComment = comment;
            this.GameResults = new List<SingleGameResult>();
        }

        public GamesSimulatorResult(IEnumerable<SingleGameResult> gameResults)
        {
            this.BattleComment = "Ready!";
            this.GameResults = gameResults;
        }

        public string BattleComment { get; }

        public IEnumerable<SingleGameResult> GameResults { get; }
    }
}
