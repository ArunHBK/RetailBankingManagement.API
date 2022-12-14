using System;
using System.Collections;
using System.Collections.Generic;
using BankManagementSystem.Models;
using BankManagementSystem.Models.DTO;

namespace BankManagementSystem.Repository.IRepository
{
    public interface IEmployeeRepo
    {
        // public Task<List<UserDetail>> ViewUserDetails();
        //public Task<List<UserLogin>> ViewLoginDetails();
        public Task<ResponseObject> Login(UserLoginDto user);
        public Task<bool> UserRegistration(UserDetailDto user);
        public Task<bool> UserDetailsUpdate(int Id, UserDetailUpdateDto user);

        public Task<bool> AccountDelete(string Username);
    }
}
