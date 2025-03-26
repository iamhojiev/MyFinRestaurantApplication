using ManagerApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ManagerApplication.Helper
{
    public static class UserListSorter
    {
        public static List<User> SortByProperty<T>(List<User> userList, Func<User, T> sortKeySelector, bool ascending = true)
        {
            return ascending ? userList.OrderBy(sortKeySelector).ToList() : userList.OrderByDescending(sortKeySelector).ToList();
        }
    }
}
