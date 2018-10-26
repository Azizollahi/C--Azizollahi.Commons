using System;

namespace AzizollahiCommons.Pooling {
	public class ThreadIdGenerator {
		private static long id;
		private static object locableObject = new object();
		public static string GetId() {
			string result;
			lock (locableObject) {
				if (id == 0xffffffff)
					id = 0;
				result = Convert.ToString(id++, 16);
			}
			return result;
		}
	}
}