cmake_minimum_required(VERSION 3.2)
set(CMAKE_BUILD_TYPE Release)
project(lua5.3.6 C)

# add_definitions(-D_CRT_SECURE_NO_WARNINGS)

set(LUA_SRC_PATH ./src)

include_directories(${LUA_SRC_PATH})

aux_source_directory(${LUA_SRC_PATH} LUA_CORE)

add_library(lua5.3.6 SHARED ${LUA_CORE})

set_target_properties(lua5.3.6 PROPERTIES DEFINE_SYMBOL  "LUA_BUILD_AS_DLL" )