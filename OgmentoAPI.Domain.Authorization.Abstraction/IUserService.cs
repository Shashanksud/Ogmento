﻿using OgmentoAPI.Domain.Authorization.Abstraction.Models;


namespace OgmentoAPI.Domain.Authorization.Abstraction
{
    public interface IUserService
    {
        ResponseModel<UserModel> Get(int UserId);
    
    }
}