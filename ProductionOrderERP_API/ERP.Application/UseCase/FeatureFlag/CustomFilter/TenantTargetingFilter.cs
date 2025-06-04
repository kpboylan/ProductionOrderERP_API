using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.FeatureFilters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class TenantTargetingFilter : IFeatureFilter
{
    private readonly IServiceProvider _serviceProvider;

    public TenantTargetingFilter(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<bool> EvaluateAsync(FeatureFilterEvaluationContext context)
    {
        using var scope = _serviceProvider.CreateScope();
        var accessor = scope.ServiceProvider.GetRequiredService<ITargetingContextAccessor>();

        var targetingContext = await accessor.GetContextAsync();
        var settings = context.Parameters.Get<TargetingFilterSettings>();

        if (targetingContext == null)
            return false;

        // Evaluate groups
        if (settings.Audience?.Groups != null)
        {
            foreach (var group in settings.Audience.Groups)
            {
                if (string.IsNullOrWhiteSpace(group.Name) || group.RolloutPercentage < 0)
                    continue;

                if (targetingContext.Groups != null && targetingContext.Groups.Contains(group.Name, StringComparer.OrdinalIgnoreCase))
                {
                    if (IsTargeted(targetingContext, group.RolloutPercentage))
                        return true;
                }
            }
        }

        // Evaluate users
        if (settings.Audience?.Users != null && targetingContext.UserId != null)
        {
            if (settings.Audience.Users.Contains(targetingContext.UserId, StringComparer.OrdinalIgnoreCase))
                return true;
        }

        // Evaluate default rollout
        //if (settings.Audience?.DefaultRolloutPercentage != null)
        //{
        //    return IsTargeted(targetingContext, settings.Audience.DefaultRolloutPercentage.Value);
        //}

        return false;
    }

    private bool IsTargeted(TargetingContext context, double percentage)
    {
        if (context.UserId == null)
            return false;

        int hash = context.UserId.GetHashCode();
        int normalized = Math.Abs(hash % 100);
        return normalized < percentage;
    }
}
