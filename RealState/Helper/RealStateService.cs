using RealState.Models;
using RealState.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static RealState.Helper.RealStateService;

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
                        UserModel.RoleId = User.RoleId;
                        UserModel.PropertyList = User.Property.Select(Property => new PropertyModel
                        {
                            Id = Property.Id,
                            BedroomNumber = Property.BedroomNumber,
                            State = Property.State,
                            City = Property.City,
                            NeighboorHood = Property.NeighboorHood,
                            StreetName = Property.StreetName,
                            HouseNumber = Property.HouseNumber,
                            Area = Property.Area,
                            UserId = Property.UserId,
                            Price = Property.Price,
                            GarageSpace = Property.GarageSpace,
                            ImagesUrl = Property.Image.Select(image => image.ImageUrl).ToList(),
                            ImagesId = Property.Image.Select(image => image.Id).ToList()
                        }).ToList();
                        //UserModel.FavoritePropertyList = User.Favorite.Select(Favorite => Favorite.PropertyId).ToList();

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
                        var User = Context.User.FirstOrDefault(f => String.Equals(f.Email, UserEmail));

                        if (User == null)
                            throw new Exception("Ocorreu um erro ao requisitar os dados de usuário.");

                        UserModel.Id = User.Id;
                        UserModel.Name = User.Name;
                        UserModel.Email = User.Email;
                        UserModel.Password = User.Password;
                        UserModel.CpfCnpj = User.CpfCnpj;
                        UserModel.RoleId = User.RoleId;
                        UserModel.PropertyList = User.Property.Select(Property => new PropertyModel
                        {
                            Id = Property.Id,
                            BedroomNumber = Property.BedroomNumber,
                            State = Property.State,
                            City = Property.City,
                            NeighboorHood = Property.NeighboorHood,
                            StreetName = Property.StreetName,
                            HouseNumber = Property.HouseNumber,
                            Area = Property.Area,
                            UserId = Property.UserId,
                            Price = Property.Price,
                            GarageSpace = Property.GarageSpace,
                            ImagesUrl = Property.Image.Select(image => image.ImageUrl).ToList(),
                            ImagesId = Property.Image.Select(image => image.Id).ToList()
                        }).ToList();
                        //UserModel.PropertyList = UsersProperties(User.Id);
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
                            RoleId = 2
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
                                    RoleId = s.RoleId
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

                        
                        Context.Favorite.RemoveRange(Context.Favorite.Where(f => f.UserId == UserId));
                        Context.Image.RemoveRange(Context.Image.Where(i => i.Property.UserId == UserId));
                        Context.Property.RemoveRange(Context.Property.Where(p => p.UserId == UserId));
                        

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

            //public List<PropertyModel> UsersProperties(int UserId)
            //{
            //    var Response = new List<PropertyModel>();
            //    var propertyHelper = new RealStateService.Property();

            //    using (var Context = new RealStateEntities())
            //    {
            //        Response = Context
            //                    .Property
            //                    .Where(s => s.UserId == UserId)
            //                    .Select(s => new PropertyModel
            //                    {
            //                        Id = s.Id,
            //                        BedroomNumber = s.BedroomNumber,
            //                        State = s.State,
            //                        City = s.City,
            //                        NeighboorHood = s.NeighboorHood,
            //                        StreetName = s.StreetName,
            //                        HouseNumber = s.HouseNumber,
            //                        Area = s.Area,
            //                        UserId = s.UserId,
            //                        Price = s.Price,
            //                        GarageSpace = s.GarageSpace,
            //                        ImagesUrl = s.Image.Select(image => image.ImageUrl).ToList()
            //                    }).ToList();
            //    }


            //    return Response;
            //}

            public LoginModel Login(string Email, string Password)
            {
                var Response = new LoginModel();
                using (var Context = new RealStateEntities())
                {
                    var User = Context.User.FirstOrDefault(f => String.Equals(f.Email, Email));

                    //var User = Get(Email);

                    if (User != null && User.Password == Password)
                    {
                        Response.Id = User.Id;
                        Response.Name = User.Name;
                        Response.Email = User.Email;
                        Response.Password = User.Password;
                        Response.CpfCnpj = User.CpfCnpj;
                        Response.RoleId = User.RoleId;
                        Response.PropertyList = User.Property.Select(Property => new PropertyModel
                        {
                            Id = Property.Id,
                            BedroomNumber = Property.BedroomNumber,
                            State = Property.State,
                            City = Property.City,
                            NeighboorHood = Property.NeighboorHood,
                            StreetName = Property.StreetName,
                            HouseNumber = Property.HouseNumber,
                            Area = Property.Area,
                            UserId = Property.UserId,
                            Price = Property.Price,
                            GarageSpace = Property.GarageSpace,
                            ImagesUrl = Property.Image.Select(image => image.ImageUrl).ToList(),
                            ImagesId = Property.Image.Select(image => image.Id).ToList()
                        }).ToList();
                        //Response.PropertyList = UsersProperties(User.Id);
                        Response.IsAuthenticated = true;
                    }
                }
                return Response;
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
                        PropertyModel.ImagesUrl = Property.Image.Select(image => image.ImageUrl).ToList();
                        PropertyModel.ImagesId = Property.Image.Select(image => image.Id).ToList();
                        //PropertyModel.ImagesUrl = PropertyImages(PropertyModel.Id);

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

            public List<PropertyModel> List(SearchModel parameters)
            {
                var Response = new List<PropertyModel>();

                using (var Context = new RealStateEntities())
                {
                    var Query = Context.Property.AsQueryable();

                    if (!String.IsNullOrEmpty(parameters.State)) 
                        Query = Query.Where(s => s.State == parameters.State);

                    if (!String.IsNullOrEmpty(parameters.City))
                        Query = Query.Where(s => s.City == parameters.City);

                    if (!String.IsNullOrEmpty(parameters.NeighboorHood)) 
                        Query = Query.Where(s => s.NeighboorHood == parameters.NeighboorHood);

                    if (!String.IsNullOrEmpty(parameters.StreetName)) 
                        Query = Query.Where(s => s.StreetName == parameters.StreetName);


                    if (parameters.Area.HasValue) 
                        Query = Query.Where(s => s.Area >= parameters.Area);

                    if (parameters.Price.HasValue) 
                        Query = Query.Where(s => s.Price <= parameters.Price);

                    if (parameters.BedroomNumber.HasValue) 
                        Query = Query.Where(s => s.BedroomNumber >= parameters.BedroomNumber);

                    if (parameters.GarageSpace.HasValue) 
                        Query = Query.Where(s => s.GarageSpace >= parameters.GarageSpace);


                    Response = Query
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
                                    GarageSpace = s.GarageSpace,

                                    ImagesUrl = s.Image.Select(image => image.ImageUrl).ToList(),
                                    ImagesId = s.Image.Select(image => image.Id).ToList()
                                }).ToList();
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

                        Context.Favorite.RemoveRange(Context.Favorite.Where(f => f.PropertyId == PropertyId));
                        Context.Image.RemoveRange(Context.Image.Where(i => i.Property.Id == PropertyId));


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

            //public List<string> PropertyImages(int PropertyId)
            //{
            //    var Response = new List<string>();

            //    using (var Context = new RealStateEntities())
            //    {
            //        Response = Context
            //                    .Image
            //                    .Where(s => s.PropertyId == PropertyId)
            //                    .Select(s => s.ImageUrl).ToList();
            //    }

            //    return Response;
            //}

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
                                    GarageSpace = s.GarageSpace,
                                    ImagesUrl = s.Image.Select(image => image.ImageUrl).ToList(),
                                }).ToList();
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
                        ImageModel.ImageUrl = Image.ImageUrl;

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
                        Entity.ImageUrl = Request.ImageUrl;

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
                            ImageUrl = Request.ImageUrl,
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
                                    ImageUrl = s.ImageUrl,
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

        public class Favorite
        {
            public FavoriteModel Get(int FavoriteId)
            {
                var FavoriteModel = new FavoriteModel();
                try
                {

                    using (var Context = new RealStateEntities())
                    {
                        var Favorite = Context.Favorite.FirstOrDefault(f => f.Id == FavoriteId);

                        if (Favorite == null)
                            throw new Exception("Ocorreu um erro ao requisitar os dados de usuário.");

                        FavoriteModel.Id = Favorite.Id;
                        FavoriteModel.PropertyId = Favorite.PropertyId;
                        FavoriteModel.UserId = Favorite.UserId;

                    }
                }
                catch (Exception Ex)
                {
                    FavoriteModel.Id = -1;
                }

                return FavoriteModel;
            }

            public bool Update(FavoriteModel Request)
            {
                var Response = false;

                try
                {
                    using (var Context = new RealStateEntities())
                    {
                        var Entity = Context.Favorite.FirstOrDefault(f => f.Id == Request.Id);

                        if (Entity == null)
                            throw new Exception("Ocorreu um erro ao editar os dados do bebe.");

                        Entity.PropertyId = Request.PropertyId;
                        Entity.UserId = Request.UserId;

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

            public bool Add(FavoriteModel Request)
            {
                var Response = false;

                try
                {
                    using (var Context = new RealStateEntities())
                    {

                        var NewFavorite = new Models.Entity.Favorite
                        {
                            PropertyId = Request.PropertyId,
                            UserId = Request.UserId,
                        };

                        Context.Favorite.Add(NewFavorite);

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

            public List<FavoriteModel> List()
            {
                var Response = new List<FavoriteModel>();

                using (var Context = new RealStateEntities())
                {
                    Response = Context
                                .Favorite
                                .Select(s => new FavoriteModel
                                {
                                    Id = s.Id,
                                    PropertyId = s.PropertyId,
                                    UserId = s.UserId,
                                }).ToList();
                }

                return Response;
            }
            public List<PropertyModel> List(int UserId)
            {
                var Response = new List<PropertyModel>();

                using (var Context = new RealStateEntities())
                {
                   Response = Context.Favorite
                                        .Where(Favorite => Favorite.UserId == UserId)
                                        .Select(Favorite => Favorite.Property)
                                        .Select(Property => new PropertyModel
                                        {
                                            Id = Property.Id,
                                            BedroomNumber = Property.BedroomNumber,
                                            State = Property.State,
                                            City = Property.City,
                                            NeighboorHood = Property.NeighboorHood,
                                            StreetName = Property.StreetName,
                                            HouseNumber = Property.HouseNumber,
                                            Area = Property.Area,
                                            UserId = Property.UserId,
                                            Price = Property.Price,
                                            GarageSpace = Property.GarageSpace,

                                            ImagesUrl = Property.Image.Select(image => image.ImageUrl).ToList(),
                                            ImagesId = Property.Image.Select(image => image.Id).ToList()
                                        }).ToList();
                }

                return Response;
            }

            public bool Remove(int FavoriteId)
            {
                var Response = false;

                try
                {
                    using (var Context = new RealStateEntities())
                    {
                        var Entity = Context.Favorite.FirstOrDefault(f => f.Id == FavoriteId);

                        if (Entity == null)
                            throw new Exception("Ocorreu um erro ao editar os dados do bebe.");

                        Context.Favorite.Remove(Entity);

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

            public FavoriteModel IsFavorite(int UserId , int PropertyId)
            {
                var FavoriteModel = new FavoriteModel();
                using ( var Context = new RealStateEntities() )
                {

                    var Favorite = Context.Favorite.FirstOrDefault(f => f.UserId == UserId && f.PropertyId == PropertyId);
                    if (Favorite == null)
                        return null;

                    FavoriteModel.Id = Favorite.Id;
                    FavoriteModel.PropertyId = Favorite.PropertyId;
                    FavoriteModel.UserId = Favorite.UserId;
                }
                return FavoriteModel;
            }
        }

    }
}