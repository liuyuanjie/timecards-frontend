using System;
using System.Collections.Generic;
using System.Linq;
using Timecards.Application;
using Timecards.Infrastructure.Model;
using TimecardsControl;

namespace Timecards.Client
{
    public class InputWorkTimeSource
    {
        private readonly List<InputWorkTime> _inputWorkTimes;

        public InputWorkTimeSource()
        {
            _inputWorkTimes = new List<InputWorkTime>();
        }

        public void Add(InputWorkTime item)
        {
            _inputWorkTimes.Add(item);
        }

        public void Remove(InputWorkTime item)
        {
            _inputWorkTimes.Remove(item);
        }

        public bool AllowSave()
        {
            return _inputWorkTimes.Any(x => x.Status == StatusType.Saved || !x.Status.HasValue);
        }

        public bool AllowSubmit()
        {
            return _inputWorkTimes.Any(x => x.Status == StatusType.Saved);
        }

        public bool AllowNew()
        {
            return !_inputWorkTimes.Any() ||
                   _inputWorkTimes.Any(x => x.Status == StatusType.Saved || !x.Status.HasValue);
        }

        public void ClearAll()
        {
            _inputWorkTimes.Clear();
        }

        public bool HasExistingProject(Guid projectId)
        {
            return _inputWorkTimes.Any(x => x.ProjectId == projectId);
        }

        public bool HasInputWorkTime()
        {
            return _inputWorkTimes.Any();
        }

        public decimal GetTotalHours()
        {
            return _inputWorkTimes.Select(x => x.SaveTimecards.Invoke()).Sum(x => x.Items.Sum(t => t.Hour));
        }

        public List<Infrastructure.Model.Timecards> ConvertTo()
        {
            var timecardses = _inputWorkTimes.Select(x =>
                new Infrastructure.Model.Timecards(x.TimecardsId, AccountStore.Account.AccountId, x.ProjectId,
                    x.TimecardsDate.ConvertToUTCDate())
                {
                    TimecardsDate = x.TimecardsDate.ConvertToUTCDate(),
                    Items = x.SaveTimecards.Invoke().Items
                        .Select(t => new ItemcardsItem(t.WorkDay.ConvertToUTCDate(), t.Hour, t.Note)).ToList()
                }).ToList();

            return timecardses;
        }

        public List<Infrastructure.Model.Timecards> GetCanSubmitTimecards()
        {
            return ConvertTo().Where(x => x.TimecardsId.HasValue).ToList();
        }

        public DateTime InputTimecardsDay()
        {
            return _inputWorkTimes.First().TimecardsDate;
        }
    }
}