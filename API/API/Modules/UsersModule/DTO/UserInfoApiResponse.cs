﻿using Domain.Accounts.Users;

namespace API.Modules.UsersModule.DTO;

public record UserInfoApiResponse(
    UserGender Gender,
    float Weight,
    int Height,
    UserTarget Target
);