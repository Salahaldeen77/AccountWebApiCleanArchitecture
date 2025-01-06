using AccountWeb.Core.Features.Authorization.Queries.Responses;
using AccountWeb.Data.Entities.Identity;

namespace AccountWeb.Core.Mapping.Roles
{
    public partial class RoleProfile
    {
        public void GetRoleByIdMapping()
        {
            CreateMap<Role, GetRoleByIdResponse>();

        }
    }
}
