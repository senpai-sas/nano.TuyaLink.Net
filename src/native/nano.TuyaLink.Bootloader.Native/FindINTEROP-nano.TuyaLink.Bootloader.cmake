#
# Copyright (c) .NET Foundation and Contributors
# See LICENSE file in the project root for full license information.
#

########################################################################################
# make sure that a valid path is set bellow                                            #
# this is an Interop module so this file should be placed in the CMakes module folder  #
# usually CMake\Modules                                                                #
########################################################################################

# native code directory
set(BASE_PATH_FOR_THIS_MODULE ${PROJECT_SOURCE_DIR}/InteropAssemblies/nano.TuyaLink.Bootloader)


# set include directories
list(APPEND nano.TuyaLink.Bootloader_INCLUDE_DIRS ${PROJECT_SOURCE_DIR}/src/CLR/Core)
list(APPEND nano.TuyaLink.Bootloader_INCLUDE_DIRS ${PROJECT_SOURCE_DIR}/src/CLR/Include)
list(APPEND nano.TuyaLink.Bootloader_INCLUDE_DIRS ${PROJECT_SOURCE_DIR}/src/HAL/Include)
list(APPEND nano.TuyaLink.Bootloader_INCLUDE_DIRS ${PROJECT_SOURCE_DIR}/src/PAL/Include)
list(APPEND nano.TuyaLink.Bootloader_INCLUDE_DIRS ${BASE_PATH_FOR_THIS_MODULE})


# source files
set(nano.TuyaLink.Bootloader_SRCS

    nano_TuyaLink_Bootloader.cpp


    nano_TuyaLink_Bootloader_nano_TuyaLink_Bootloader_TuyaBootloader_mshl.cpp
    nano_TuyaLink_Bootloader_nano_TuyaLink_Bootloader_TuyaBootloader.cpp

)

foreach(SRC_FILE ${nano.TuyaLink.Bootloader_SRCS})

    set(nano.TuyaLink.Bootloader_SRC_FILE SRC_FILE-NOTFOUND)

    find_file(nano.TuyaLink.Bootloader_SRC_FILE ${SRC_FILE}
        PATHS
	        ${BASE_PATH_FOR_THIS_MODULE}
	        ${TARGET_BASE_LOCATION}
            ${PROJECT_SOURCE_DIR}/src/nano.TuyaLink.Bootloader

	    CMAKE_FIND_ROOT_PATH_BOTH
    )

    if (BUILD_VERBOSE)
        message("${SRC_FILE} >> ${nano.TuyaLink.Bootloader_SRC_FILE}")
    endif()

    list(APPEND nano.TuyaLink.Bootloader_SOURCES ${nano.TuyaLink.Bootloader_SRC_FILE})

endforeach()

include(FindPackageHandleStandardArgs)

FIND_PACKAGE_HANDLE_STANDARD_ARGS(nano.TuyaLink.Bootloader DEFAULT_MSG nano.TuyaLink.Bootloader_INCLUDE_DIRS nano.TuyaLink.Bootloader_SOURCES)
