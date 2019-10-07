using System;

namespace Application.Api.Services
{
  public class PassowordGenerator
  {
    public string Generate()
    {
      string caracteresPermitidos = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789!@$?-";
      char[] chars = new char[8];

      Random rd = new Random();
      for (int i = 0; i < 8; i++)
      {
        chars[i] = caracteresPermitidos[rd.Next(0, caracteresPermitidos.Length)];
      }

      return new string(chars);
    }
  }
}
