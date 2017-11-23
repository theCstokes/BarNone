using BarNone.Shared.Core;
using BarNone.TheRack.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.DataConverter
{
    public interface IConverter
    {
        ShareDataConverterCache Cache { get; }
    }

    //public abstract class BaseConverter
    //{
    //    public ShareDataConverterCache Cache { get; private set; }
    //    private Converters()
    //    {
    //        Cache = new ShareDataConverterCache();
    //        Init();
    //    }

    //    public UserConverter User { get; private set; }

    //    public static Converters Convert
    //    {
    //        get
    //        {
    //            return new Converters();
    //        }
    //    }

    //    private void Init()
    //    {
    //        User = new UserConverter(this);
    //    }
    //}

    //public class ConverterContext
    //{
    //    public UserConverter UserConverter { get; set; }


    //}

    //public class User : ITrackable<User>
    //{
    //    public int ID { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    //}

    //public class UserDTO : ITrackableDTO<UserDTO>
    //{
    //    public int ID { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    //}

    //public class UserConverter : BaseDataConverter<User, UserDTO>
    //{
    //    public UserConverter(Converters converters) : base(converters)
    //    {

    //    }

    //    public override User OnCreateDataModel(UserDTO dto)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override UserDTO OnCreateDTO(User data)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
