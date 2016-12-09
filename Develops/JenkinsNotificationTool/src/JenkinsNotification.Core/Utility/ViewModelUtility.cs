namespace JenkinsNotification.Core.Utility
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using JenkinsNotification.Core.ComponentModels;

    public static class ViewModelUtility
    {
        public static bool Validates(IEnumerable<ViewModelBase> viewModels, Func<ViewModelBase, bool> predicate)
        {
            return Validates(viewModels.Where(predicate));
        }

        public static bool Validates(IEnumerable<ViewModelBase> viewModels)
        {
            foreach (var viewModel in viewModels)
            {
                viewModel.Validate();
            }

            return viewModels.Any(x => x.HasErrors);
        }
    }
}