﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Umbraco.Core.Configuration.HealthChecks;

namespace Umbraco.Configuration.Models
{
    internal class HealthChecksSettings : IHealthChecksSettings
    {
        private readonly IConfiguration _configuration;
        public HealthChecksSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IEnumerable<IDisabledHealthCheck> DisabledChecks => _configuration.GetSection("Umbraco:CMS:HealthChecks:DisabledChecks").GetChildren().Select(
            x => new DisabledHealthCheck()
            {
                Id = x.GetValue<Guid>("Id"),
                DisabledOn = x.GetValue<DateTime>("DisabledOn"),
                DisabledBy = x.GetValue<int>("DisabledBy"),
            });
        public IHealthCheckNotificationSettings NotificationSettings => new HealthCheckNotificationSettings(_configuration.GetSection("Umbraco:CMS:HealthChecks:NotificationSettings"));

        private class DisabledHealthCheck : IDisabledHealthCheck
        {
            public Guid Id { get; set; }
            public DateTime DisabledOn { get;set; }
            public int DisabledBy { get;set; }
        }

        private class HealthCheckNotificationSettings : IHealthCheckNotificationSettings
        {
            private readonly IConfigurationSection _configurationSection;

            public HealthCheckNotificationSettings(IConfigurationSection configurationSection)
            {
                _configurationSection = configurationSection;
            }

            public bool Enabled => _configurationSection.GetValue<bool>("Enabled", false);
            public string FirstRunTime => _configurationSection.GetValue<string>("FirstRunTime");
            public int PeriodInHours => _configurationSection.GetValue<int>("PeriodInHours", 24);

            public IReadOnlyDictionary<string, INotificationMethod> NotificationMethods => _configurationSection
                .GetSection("NotificationMethods")
                .GetChildren()
                .ToDictionary(x=>x.Key, x=> (INotificationMethod) new NotificationMethod(x.Key, x));

            public IEnumerable<IDisabledHealthCheck> DisabledChecks => _configurationSection.GetSection("DisabledChecks").GetChildren().Select(
                x => new DisabledHealthCheck()
                {
                    Id = x.GetValue<Guid>("Id"),
                    DisabledOn = x.GetValue<DateTime>("DisabledOn"),
                    DisabledBy = x.GetValue<int>("DisabledBy"),
                });
        }

        private class NotificationMethod : INotificationMethod
        {
            private readonly IConfigurationSection _configurationSection;

            public NotificationMethod(string alias,  IConfigurationSection configurationSection)
            {
                Alias = alias;
                _configurationSection = configurationSection;
            }

            public string Alias { get; }
            public bool Enabled => _configurationSection.GetValue<bool>("Enabled", false);
            public HealthCheckNotificationVerbosity Verbosity  => _configurationSection.GetValue<HealthCheckNotificationVerbosity>("Verbosity", HealthCheckNotificationVerbosity.Summary);
            public bool FailureOnly => _configurationSection.GetValue<bool>("FailureOnly", true);

            public IReadOnlyDictionary<string, INotificationMethodSettings> Settings => _configurationSection
                .GetSection("Settings").GetChildren().ToDictionary(x => x.Key, x => (INotificationMethodSettings)new NotificationMethodSettings(x.Key, x.Value));
        }

        private class NotificationMethodSettings : INotificationMethodSettings
        {
            public NotificationMethodSettings(string key, string value)
            {
                Key = key;
                Value = value;
            }

            public string Key { get; }
            public string Value { get; }
        }
    }
}
