#include <iostream>
#include <windows.h>
#include "logger.h"

const std::wstring programName = L"singleInstance";
const std::wstring programName = L"singleInstance";
const std::wstring robloxMutexName = L"ROBLOX_singletonMutex";
Logger& logger = Logger::getInstance();

void minimizeWindow()
{
	HWND window = GetConsoleWindow();

	while (!IsWindowVisible(window))
	{
		Sleep(0);
	}

	ShowWindow(window, SW_MINIMIZE);
}

int main()
{
	SetConsoleTitle(programName.c_str());

	HANDLE programMutex = CreateMutexW(NULL, true, programName.c_str());

	if (GetLastError() == ERROR_ALREADY_EXISTS)
	{
		return -1;
	}

	logger.log(LogLevel::Info, "starting...");

	HANDLE existingRobloxMutex = OpenMutexW(SYNCHRONIZE, false, robloxMutexName.c_str());

	if (existingRobloxMutex)
	{
		logger.log(LogLevel::Warning, "waiting for roblox to get closed..");

		DWORD result = WaitForSingleObject(existingRobloxMutex, INFINITE);

		if (result != WAIT_OBJECT_0 && result != WAIT_ABANDONED)
		{
			logger.logFailure();
			return -1;
		}
	}
	else
	{
		HANDLE newRobloxMutex = CreateMutexW(NULL, true, robloxMutexName.c_str());

		if (!newRobloxMutex)
		{
			logger.logFailure();
			return -1;
		}
	}

	minimizeWindow();
	logger.log(LogLevel::Success, "ok (close me to stop)");
	std::cin.get();
	return 0;
}