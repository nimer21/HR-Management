using AutoMapper;
using HR_Management.Models;
using HR_Management.ViewModels;

namespace HR_Management.Profiles
{
    public class AutomapperProfiles:Profile
    {
        public AutomapperProfiles()
        {
            CreateMap<Employee,EmployeeViewModel>().ReverseMap();
        }
    }
}
