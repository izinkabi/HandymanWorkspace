﻿namespace Handyman_DataLibrary.DataAccess.Interfaces;

public interface ITokenProvider
{
    string GenerateToken(string email);
}