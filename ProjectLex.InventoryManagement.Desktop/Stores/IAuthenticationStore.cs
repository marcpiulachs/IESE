using ProjectLex.InventoryManagement.Database.Models;
using System;

namespace ProjectLex.InventoryManagement.Desktop.Stores
{
    public interface IAuthenticationStore
    {
        Staff CurrentStaff { get; set; }
        bool IsLoggedIn { get; set; }

        event Action IsCurrentStaffChanged;
        event Action IsLoggedInChanged;
    }
}