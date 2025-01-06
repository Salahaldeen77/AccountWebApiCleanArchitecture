using AutoMapper;

namespace AccountWeb.Core.Mapping.Roles
{
    public partial class RoleProfile : Profile
    {
        public RoleProfile()
        {
            GetRolesListMapping();
            GetRoleByIdMapping();
        }
    }
}
