using System.Runtime.Serialization;

namespace MyApp.Common.Framework;

[DataContract]
public class Result
{
    [DataMember]
    public bool Successful { get; }
    [DataMember]
    public IList<Alert> Alerts { get; }

    protected Result(bool successful, IEnumerable<Alert> alerts)
    {
        Successful = successful;
        Alerts = alerts.ToList();
    }

    public static Result Success(params Alert[] alerts) => new Result(true, alerts);
    public static Result Success(IEnumerable<Alert> alerts) => new Result(true, alerts);
    public static Result Failure(params Alert[] alerts) => new Result(false, alerts);
    public static Result Failure(IEnumerable<Alert> alerts) => new Result(false, alerts);
    public static Result Failure(params string[] errors) =>
        new Result(false, errors.Select(e => Alert.Error(e)));
    public static Result Failure(IEnumerable<string> errors) =>
        new Result(false, errors.Select(e => Alert.Error(e)));

    public static Result<T> Success<T>(T data) => new Result<T>(true, data, Array.Empty<Alert>());
    public static Result<T> Success<T>(T data, params Alert[] alerts) => new Result<T>(true, data, alerts);
    public static Result<T> Success<T>(T data, IEnumerable<Alert> alerts) => new Result<T>(true, data, alerts);
    public static Result<T> Failure<T>(IEnumerable<Alert> alerts) =>
        new Result<T>(false, default, alerts);
    public static Result<T> Failure<T>(params string[] errors) =>
        new Result<T>(false, default, errors.Select(e => Alert.Error(e)));
    public static Result<T> Failure<T>(IEnumerable<string> errors) =>
        new Result<T>(false, default, errors.Select(e => Alert.Error(e)));
}

[DataContract]
public class Result<T> : Result
{
    [DataMember]
    public T? Data { get; set; }

    internal Result(bool successful, T? data, IEnumerable<Alert> alerts)
        : base(successful, alerts)
    {
        Data = data;
    }

    public Result<TNewType> Map<TNewType>(Func<T, TNewType> mapper)
    {
        return new Result<TNewType>(
            Successful,
            Data == null ? default : mapper(Data),
            Alerts);
    }
}
