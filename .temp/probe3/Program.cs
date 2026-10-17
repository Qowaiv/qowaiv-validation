using Qowaiv;
using Qowaiv.Validation.DataAnnotations;
using Qowaiv.Validation.TestData;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

var child = new BenchmarkModel.Child();
var email = child.Email;
var model = BenchmarkModel.New(10);

var provider = new Provider();
var items = new Dictionary<object, object?>();
var mand = new MandatoryAttribute();
var any = new AnyAttribute();
var atLeast = new Length.AtLeastAttribute(6);
var list = model.Children;

var ctxEmail = new ValidationContext(child, provider, items) { MemberName = nameof(BenchmarkModel.Child.Email) };
var ctxName = new ValidationContext(model, provider, items) { MemberName = nameof(BenchmarkModel.RequiredName) };

Measure("Mandatory(Email)", () => mand.GetValidationMessage(email, ctxEmail));
Measure("Any(Children)", () => any.GetValidationMessage(list, ctxName));
Measure("Length.AtLeast(str)", () => atLeast.GetValidationMessage("123456", ctxName));
Measure("Type.GetProperty(Email)", () => _ = typeof(BenchmarkModel.Child).GetProperty("Email"));
Measure("QowaivType.GetNotNullableType", () => _ = Qowaiv.Reflection.QowaivType.GetNotNullableType(typeof(EmailAddress)));
Measure("Unknown.Value(EmailAddress)", () => _ = Qowaiv.Unknown.Value(typeof(EmailAddress)));

static void Measure(string name, Action act)
{
    for (var i = 0; i < 5000; i++) act();
    GC.Collect(); GC.WaitForPendingFinalizers(); GC.Collect();
    var n = 200_000;
    var alloc0 = GC.GetAllocatedBytesForCurrentThread();
    var sw = Stopwatch.StartNew();
    for (var i = 0; i < n; i++) act();
    sw.Stop();
    var alloc = GC.GetAllocatedBytesForCurrentThread() - alloc0;
    Console.WriteLine($"{name,-32} {sw.Elapsed.TotalMilliseconds * 1000 / n,10:F3} us/call   {alloc / n,8:F0} B/call");
}

sealed class Provider : IServiceProvider
{
    public object? GetService(Type serviceType) => null;
}
