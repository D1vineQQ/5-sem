#include <iostream>



int main()
{
	time_t t;
	tm tm;

	time(&t);
	localtime_s(&tm, &t);
	std::cout << t << std::endl;
	printf(
		"%2d.%02d.%4d %2d:%2d:%2d",
		tm.tm_mday,
		tm.tm_mon + 1,
		tm.tm_year + 1900,
		tm.tm_hour,
		tm.tm_min,
		tm.tm_sec
	);
}