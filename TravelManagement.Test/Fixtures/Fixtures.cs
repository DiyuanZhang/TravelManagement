using System;

namespace TravelManagement.Test.Fixtures
{
	internal static partial class Fixtures
	{
		internal static BuildWrapper<T> Build<T>(T t)
		{
			return new BuildWrapper<T>(t);
		}

		internal class BuildWrapper<T>
		{
			private readonly T t;

			public BuildWrapper(T t)
			{
				this.t = t;
			}

			public BuildWrapper<T> With(Action<T> action)
			{
				action(t);
				return this;
			}

			public T Default()
			{
				return t;
			}
		}
	}
}