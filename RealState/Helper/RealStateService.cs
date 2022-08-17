using RealState.Models;
using RealState.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealState.Helper
{
    public class RealStateService
    {
        public class User
        {
            public UserModel Get(int UserId)
            {
                var UserModel = new UserModel();
                try
                {

                    using (var Context = new RealStateEntities())
                    {
                        var User = Context.User.FirstOrDefault(f => f.Id == UserId);

                        if (User == null)
                            throw new Exception("Ocorreu um erro ao requisitar os dados de usuário.");

                        UserModel.Id = User.Id;
                        UserModel.Name = User.Name;
                        UserModel.Email = User.Email;
                        UserModel.Password = User.Password;
                        UserModel.CpfCnpj = User.CpfCnpj;
                        UserModel.Role = User.Role;
                        UserModel.PropertyList = UsersProperties(User.Id);
                    }
                }
                catch (Exception Ex)
                {
                    UserModel.Id = -1;
                }

                return UserModel;
            }

            public UserModel Get(string UserEmail)
            {
                var UserModel = new UserModel();
                try
                {

                    using (var Context = new RealStateEntities())
                    {
                        var User = Context.User.FirstOrDefault(f => String.Equals(f.Email,UserEmail));

                        if (User == null)
                            throw new Exception("Ocorreu um erro ao requisitar os dados de usuário.");

                        UserModel.Id = User.Id;
                        UserModel.Name = User.Name;
                        UserModel.Email = User.Email;
                        UserModel.Password = User.Password;
                        UserModel.CpfCnpj = User.CpfCnpj;
                        UserModel.Role = User.Role;
                        UserModel.PropertyList = UsersProperties(User.Id);
                    }
                }
                catch (Exception Ex)
                {
                    UserModel.Id = -1;
                }

                return UserModel;
            }

            public bool Update(UserModel Request)
            {
                var Response = false;

                try
                {
                    using (var Context = new RealStateEntities())
                    {
                        var Entity = Context.User.FirstOrDefault(f => f.Id == Request.Id);

                        if (Entity == null)
                            throw new Exception("Ocorreu um erro ao editar os dados do bebe.");

                        Entity.Name = Request.Name;
                        Entity.Email = Request.Email;
                        Entity.Password = Request.Password;
                        Entity.CpfCnpj = Request.CpfCnpj;

                        Response = true;

                        Context.SaveChanges();
                    }
                }
                catch (Exception Ex)
                {
                    Response = false;
                }


                return Response;
            }

            public bool Add(UserModel Request)
            {
                var Response = false;

                try
                {
                    using (var Context = new RealStateEntities())
                    {

                        var NewUser = new Models.Entity.User
                        {
                            Name = Request.Name,
                            Email = Request.Email,
                            Password = Request.Password,
                            CpfCnpj = Request.CpfCnpj,
                            Role = 2
                        };

                        Context.User.Add(NewUser);

                        Context.SaveChanges();

                        Response = true;

                    }
                }
                catch (Exception Ex)
                {
                    Response = false;
                }


                return Response;
            }

            public List<UserModel> List()
            {
                var Response = new List<UserModel>();

                using (var Context = new RealStateEntities())
                {
                    Response = Context
                                .User
                                .Select(s => new UserModel
                                {
                                    Id = s.Id,
                                    Name = s.Name,
                                    Email = s.Email,
                                    Password = s.Password,
                                    CpfCnpj = s.CpfCnpj,
                                    Role = s.Role
                                }).ToList();
                }

                return Response;
            }

            public bool Remove(int UserId)
            {
                var Response = false;

                try
                {
                    using (var Context = new RealStateEntities())
                    {
                        var Entity = Context.User.FirstOrDefault(f => f.Id == UserId);

                        if (Entity == null)
                            throw new Exception("Ocorreu um erro ao editar os dados do bebe.");

                        Context.User.Remove(Entity);

                        Context.SaveChanges();

                        Response = true;
                    }
                }
                catch (Exception Ex)
                {
                    Response = false;
                }


                return Response;
            }

            public List<PropertyModel> UsersProperties(int UserId)
            {
                var Response = new List<PropertyModel>();
                var propertyHelper = new RealStateService.Property();

                using (var Context = new RealStateEntities())
                {
                    Response = Context
                                .Property
                                .Where(s => s.UserId == UserId)
                                .Select(s => new PropertyModel
                                {
                                    Id = s.Id,
                                    BedroomNumber = s.BedroomNumber,
                                    State = s.State,
                                    City = s.City,
                                    NeighboorHood = s.NeighboorHood,
                                    StreetName = s.StreetName,
                                    HouseNumber = s.HouseNumber,
                                    Area = s.Area,
                                    UserId = s.UserId,
                                    Price = s.Price,
                                    GarageSpace = s.GarageSpace
                                }).ToList();
                }

                foreach (var property in Response)
                {
                    property.ImagesByteCode = propertyHelper.PropertyImages(property.Id);
                }

                return Response;
            }

            public bool Login(UserModel UserLogin)
            {

                using (var Context = new RealStateEntities())
                {
                    var User = Context.User.FirstOrDefault(f => String.Equals(f.Email,UserLogin.Email));

                    if (User == null)
                        return false;
                    if (!String.Equals(User.Password , UserLogin.Password))
                        return false;

                }

                return true;
            }
        }

        public class Property
        {
            public PropertyModel Get(int PropertyId)
            {
                var PropertyModel = new PropertyModel();
                try
                {

                    using (var Context = new RealStateEntities())
                    {
                        var Property = Context.Property.FirstOrDefault(f => f.Id == PropertyId);

                        if (Property == null)
                            throw new Exception("Ocorreu um erro ao requisitar os dados de usuário.");

                        PropertyModel.Id = Property.Id;
                        PropertyModel.BedroomNumber = Property.BedroomNumber;
                        PropertyModel.State = Property.State;
                        PropertyModel.City = Property.City;
                        PropertyModel.NeighboorHood = Property.NeighboorHood;
                        PropertyModel.StreetName = Property.StreetName;
                        PropertyModel.HouseNumber = Property.HouseNumber;
                        PropertyModel.Area = Property.Area;
                        PropertyModel.UserId = Property.UserId;
                        PropertyModel.Price = Property.Price;
                        PropertyModel.GarageSpace = Property.GarageSpace;
                        PropertyModel.ImagesByteCode = PropertyImages(PropertyModel.Id);

                    }
                }
                catch (Exception Ex)
                {
                    PropertyModel.Id = -1;
                }

                return PropertyModel;
            }

            public bool Update(PropertyModel Request)
            {
                var Response = false;

                try
                {
                    using (var Context = new RealStateEntities())
                    {
                        var Entity = Context.Property.FirstOrDefault(f => f.Id == Request.Id);

                        if (Entity == null)
                            throw new Exception("Ocorreu um erro ao editar os dados do bebe.");

                        Entity.BedroomNumber = Request.BedroomNumber;
                        Entity.State = Request.State;
                        Entity.City = Request.City;
                        Entity.NeighboorHood = Request.NeighboorHood;
                        Entity.StreetName = Request.StreetName;
                        Entity.HouseNumber = Request.HouseNumber;
                        Entity.Area = Request.Area;
                        Entity.UserId = Request.UserId;
                        Entity.Price = Request.Price;
                        Entity.GarageSpace = Request.GarageSpace;

                        Context.SaveChanges();

                        Response = true;

                    }
                }
                catch (Exception Ex)
                {
                    Response = false;
                }


                return Response;
            }

            public int Add(PropertyModel Request)
            {
                int Response;

                try
                {
                    using (var Context = new RealStateEntities())
                    {

                        var NewProperty = new Models.Entity.Property
                        {
                            BedroomNumber = Request.BedroomNumber,
                            State = Request.State,
                            City = Request.City,
                            NeighboorHood = Request.NeighboorHood,
                            StreetName = Request.StreetName,
                            HouseNumber = Request.HouseNumber,
                            Area = Request.Area,
                            UserId = Request.UserId,
                            Price = Request.Price,
                            GarageSpace = Request.GarageSpace
                        };

                        Context.Property.Add(NewProperty);

                        Context.SaveChanges();

                        Response = NewProperty.Id;
                    }
                }
                catch (Exception Ex)
                {
                    Response = -1;
                }


                return Response;
            }

            public List<PropertyModel> List()
            {
                var Response = new List<PropertyModel>();

                using (var Context = new RealStateEntities())
                {
                    Response = Context
                                .Property
                                .Select(s => new PropertyModel
                                {
                                    Id = s.Id,
                                    BedroomNumber = s.BedroomNumber,
                                    State = s.State,
                                    City = s.City,
                                    NeighboorHood = s.NeighboorHood,
                                    StreetName = s.StreetName,
                                    HouseNumber = s.HouseNumber,
                                    Area = s.Area,
                                    UserId = s.UserId,
                                    Price = s.Price,
                                    GarageSpace = s.GarageSpace
                                }).ToList();
                }

                foreach(var property in Response)
                {
                    property.ImagesByteCode = PropertyImages(property.Id);
                }

                return Response;
            }

            public bool Remove(int PropertyId)
            {
                var Response = false;

                try
                {
                    using (var Context = new RealStateEntities())
                    {
                        var Entity = Context.Property.FirstOrDefault(f => f.Id == PropertyId);

                        if (Entity == null)
                            throw new Exception("Ocorreu um erro ao editar os dados do bebe.");

                        Context.Property.Remove(Entity);

                        Context.SaveChanges();

                        Response = true;
                    }
                }
                catch (Exception Ex)
                {
                    Response = false;
                }


                return Response;
            }

            public List<string> PropertyImages(int PropertyId)
            {
                var Response = new List<string>();

                using (var Context = new RealStateEntities())
                {
                    Response = Context
                                .Image
                                .Where(s => s.PropertyId == PropertyId)
                                .Select(s => s.ByteCodeBase64).ToList();
                }

                return Response;
            }

            public List<PropertyModel> MostRecent(int amount)
            {
                var Response = new List<PropertyModel>();

                using (var context = new RealStateEntities())
                {
                    Response = context
                                .Property
                                .OrderByDescending(s => s.Id)
                                .Take(amount)
                                .Select(s => new PropertyModel
                                {
                                    Id = s.Id,
                                    BedroomNumber = s.BedroomNumber,
                                    State = s.State,
                                    City = s.City,
                                    NeighboorHood = s.NeighboorHood,
                                    StreetName = s.StreetName,
                                    HouseNumber = s.HouseNumber,
                                    Area = s.Area,
                                    UserId = s.UserId,
                                    Price = s.Price,
                                    GarageSpace = s.GarageSpace
                                }).ToList();
                                
                }
                foreach (var property in Response)
                {
                    property.ImagesByteCode = PropertyImages(property.Id);
                }

                return Response;
            }

        }

        public class Image
        {
            public ImageModel Get(int ImageId)
            {
                var ImageModel = new ImageModel();
                try
                {

                    using (var Context = new RealStateEntities())
                    {
                        var Image = Context.Image.FirstOrDefault(f => f.Id == ImageId);

                        if (Image == null)
                            throw new Exception("Ocorreu um erro ao requisitar os dados de usuário.");

                        ImageModel.Id = Image.Id;
                        ImageModel.PropertyId = Image.PropertyId;
                        ImageModel.ByteCodeBase64 = Image.ByteCodeBase64;

                    }
                }
                catch (Exception Ex)
                {
                    ImageModel.Id = -1;
                }

                return ImageModel;
            }

            public bool Update(ImageModel Request)
            {
                var Response = false;

                try
                {
                    using (var Context = new RealStateEntities())
                    {
                        var Entity = Context.Image.FirstOrDefault(f => f.Id == Request.Id);

                        if (Entity == null)
                            throw new Exception("Ocorreu um erro ao editar os dados do bebe.");

                        Entity.PropertyId = Request.PropertyId;
                        Entity.ByteCodeBase64 = Request.ByteCodeBase64;

                        Response = true;

                        Context.SaveChanges();
                    }
                }
                catch (Exception Ex)
                {
                    Response = false;
                }


                return Response;
            }

            public bool Add(ImageModel Request)
            {
                var Response = false;

                try
                {
                    using (var Context = new RealStateEntities())
                    {

                        var NewImage = new Models.Entity.Image
                        {
                            PropertyId = Request.PropertyId,
                            ByteCodeBase64 = Request.ByteCodeBase64,
                        };

                        Context.Image.Add(NewImage);

                        Context.SaveChanges();

                        Response = true;

                    }
                }
                catch (Exception Ex)
                {
                    Response = false;
                }


                return Response;
            }

            public List<ImageModel> List()
            {
                var Response = new List<ImageModel>();

                using (var Context = new RealStateEntities())
                {
                    Response = Context
                                .Image
                                .Select(s => new ImageModel
                                {
                                    Id = s.Id,
                                    PropertyId = s.PropertyId,
                                    ByteCodeBase64 = s.ByteCodeBase64,
                                }).ToList();
                }

                return Response;
            }

            public bool Remove(int ImageId)
            {
                var Response = false;

                try
                {
                    using (var Context = new RealStateEntities())
                    {
                        var Entity = Context.Image.FirstOrDefault(f => f.Id == ImageId);

                        if (Entity == null)
                            throw new Exception("Ocorreu um erro ao editar os dados do bebe.");

                        Context.Image.Remove(Entity);

                        Context.SaveChanges();

                        Response = true;
                    }
                }
                catch (Exception Ex)
                {
                    Response = false;
                }


                return Response;
            }
        }

    }
}