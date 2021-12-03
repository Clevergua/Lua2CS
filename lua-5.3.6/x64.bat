mkdir x64 & pushd x64
cmake -G "Visual Studio 16 2019" -A x64 ..
popd
cmake --build x64 --config Release
pause