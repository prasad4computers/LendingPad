﻿using System;
using System.Collections.Generic;
using System.Linq;
using BusinessEntities;
using Common;
using Data.Repositories;

namespace Core.Services.Users
{
    [AutoRegister]
    public class GetUserService : IGetUserService
    {
        private readonly IUserRepository _userRepository;

        public GetUserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User GetUser(Guid id)
        {
            return _userRepository.Get(id);
        }

        public User GetUserByTag(string tag)
        {
            return _userRepository.GetUserByTag(tag);
        }

        public IEnumerable<User> GetUsers(UserTypes? userType = null, string name = null, string email = null)
        {
            return _userRepository.Get(userType, name, email);
        }
    }
}