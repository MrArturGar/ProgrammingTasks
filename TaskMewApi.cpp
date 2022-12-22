#define CURL_STATICLIB
#include <iostream>
#include <fstream>
#include <string>
#include <algorithm>
#include "curl/curl.h"
#include <vector>

using namespace std;

vector<string> output;

static size_t header_callback(char* buffer, size_t size,
	size_t nitems, void* userdata)
{
	string s(buffer, size * nitems);
	int delimiter = s.find(": ");
	string name = s.substr(0, delimiter);
	transform(name.begin(), name.end(), name.begin(), ::tolower);
	string value;

	if (delimiter != -1 && name == "x-cat-value")
	{
		value = s.substr(delimiter + 2, s.size() - delimiter - 2);
		value.erase(std::remove(value.begin(), value.end(), '\r'), value.end());
		output.push_back(value);
	}
	return size * nitems;
}

void ReadInputFile(string* args)
{
	ifstream in("input.txt");
	if (in.is_open())
	{
		int lineCount = 0;

		std::string line;
		while (getline(in, line) && lineCount < 4)
		{
			args[lineCount++] = "X-Cat-Variable: " + line;
		}

	}
	in.close();
}

void SendRequest(curl_slist* headers)
{
	output.clear();
	CURL* curl;
	CURLcode response;
	curl = curl_easy_init();
	curl_easy_setopt(curl, CURLOPT_URL, "http://127.0.0.1:7777");//????????????????????
	curl_easy_setopt(curl, CURLOPT_CUSTOMREQUEST, "MEW");
	curl_easy_setopt(curl, CURLOPT_HEADER, 1);
	curl_easy_setopt(curl, CURLOPT_HTTPHEADER, headers);
	curl_easy_setopt(curl, CURLOPT_HEADERFUNCTION, header_callback);
	response = curl_easy_perform(curl);
	curl_easy_cleanup(curl);
}

int GetIndexItemDuplicate(vector<string> main)
{
	for (int i = 0; i < main.size(); i++)
		for (int j = 0; j < output.size(); j++)
			if (main[i] == output[j]) {
				output.erase(output.begin()+j);
				return i;
			}
}

int main()
{
	struct curl_slist* headers = NULL;
	vector<string> outputBuffer;
	string args[4];
	string outputFile = "";
	ReadInputFile(args);


	//Check items 0 and 1
	headers = curl_slist_append(headers, args[0].c_str());
	headers = curl_slist_append(headers, args[1].c_str());
	SendRequest(headers);
	headers = NULL;
	outputBuffer = output;

	//Check items 0 and 2
	headers = curl_slist_append(headers, args[0].c_str());
	headers = curl_slist_append(headers, args[2].c_str());
	SendRequest(headers);
	headers = NULL;

	swap(outputBuffer[GetIndexItemDuplicate(outputBuffer)], outputBuffer[0]);
	outputFile = outputBuffer[0] + outputBuffer[1] + output[0];

	//Check item 3
	headers = curl_slist_append(headers, args[3].c_str());
	SendRequest(headers);
	headers = NULL;
	outputFile += output[0];

	if (!output.empty()) {
		std::ofstream out("output.txt");// , std::ios::app);
		if (out.is_open())
		{
			out << outputFile << std::endl;
		}
		out.close();
	}
	return 0;
}
