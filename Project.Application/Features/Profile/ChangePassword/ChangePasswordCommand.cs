﻿using MediatR;

namespace Project.Application.Features.Profile.ChangePassword;

public sealed record ChangePasswordCommand(string currentPassword, string password, string confirmPassword) : IRequest;