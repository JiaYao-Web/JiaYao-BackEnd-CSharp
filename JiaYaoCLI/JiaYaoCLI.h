#pragma once

using namespace System;

namespace JiaYaoCLI {
	public ref class IDGenerator
	{
	public:
		static String^ generate_id() {
			DateTime^ currentTime = gcnew System::DateTime;
			currentTime = System::DateTime::Now;
			String^ time_ticks = currentTime->Ticks.ToString();
			Random^ generator = gcnew Random;
			String^ d = generator->Next(10, 99).ToString();
			String^ result = time_ticks + d;
			return result;
		}
	};
}
