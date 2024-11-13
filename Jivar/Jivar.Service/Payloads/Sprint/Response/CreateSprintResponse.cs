﻿namespace Jivar.Service.Payloads.Sprint.Response
{
    public class CreateSprintResponse
    {
        public int Id { get; set; }

        public int? ProjectId { get; set; }

        public string? Name { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

    }
}
