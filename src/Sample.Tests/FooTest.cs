using System;
using System.Reflection;
using Xunit;

namespace Sample.Tests
{
    public class Bar
    {
    }

    public class Baz
    {
    }

    public class Quux
    {
    }

    public class FooTest
    {
        [Fact]
        public void Test0()
        {
            // Hoge は Sample.csproj で public なクラス
            // テスト対象の Assembly を取得する方法はいくつかあるがライブラリのテストであれば
            // プロジェクトの参照を持っているだろうし public なクラスもあるはずなので GetAssembly で取得するのが楽
            var asm = Assembly.GetAssembly(typeof(Hoge));
            var t = typeof(Bar);
            // Generics の場合はパラメータとして与えられた型の個数を「`」の後に指定する
            // そして具体的に Generics のパラメータにどのような型を指定するかは
            // 型の最後に[[{型の名前1}],[{型の名前2}],...,[{型の名前n}]]のように指定する
            var type = asm.GetType("Sample.Foo`2[[Sample.Tests.Bar, Sample.Tests],[Sample.Tests.Bar, Sample.Tests]]");

            // 取得した type を使用して CreateInstance でインスタンスを作成する
            var instance = Activator.CreateInstance(type);

            Assert.NotNull(instance);
        }

        [Fact]
        public void Test1()
        {
            var asm = Assembly.GetAssembly(typeof(Hoge));

            var type = asm.GetType("Sample.Foo`2").MakeGenericType(typeof(Bar), typeof(Baz));

            var instance = Activator.CreateInstance(type);

            Assert.NotNull(instance);
        }

        [Fact]
        public void Test2()
        {
            var asm = Assembly.GetAssembly(typeof(Hoge));

            // Nested Type の場合 Declaring Type の後に「+」を付けてそのあとに名前を指定します
            // Generics の具体的な型の指定は Declaring Type も Nested Type もまとめて配列で指定します
            var type = asm.GetType("Sample.Qux`1+Foo`2[[Sample.Tests.Quux, Sample.Tests],[Sample.Tests.Bar, Sample.Tests],[Sample.Tests.Baz, Sample.Tests]]");

            var instance = Activator.CreateInstance(type);

            Assert.NotNull(instance);
        }

        [Fact]
        public void Test3()
        {
            var asm = Assembly.GetAssembly(typeof(Hoge));

            var type = asm.GetType("Sample.Qux`1+Foo`2").MakeGenericType(typeof(Quux), typeof(Bar), typeof(Baz));

            var instance = Activator.CreateInstance(type);

            Assert.NotNull(instance);
        }
    }
}
