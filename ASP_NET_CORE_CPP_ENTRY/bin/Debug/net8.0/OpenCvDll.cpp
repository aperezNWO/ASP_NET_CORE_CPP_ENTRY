/////////////////////////////////////////////////////////
//  GET STATIC FILES OF OPENCV C++ MINGW X64
/////////////////////////////////////////////////////////

/*

////////////////////////////////////////////////////////////////////////////
// custonm, worked !!! but not *.a file generated
////////////////////////////////////////////////////////////////////////////

git clone https://github.com/opencv/opencv.git
git clone https://github.com/opencv/opencv_contrib.git

md build
cd build

set CC=C:/MinGW/mingw64/bin/gcc.exe
set CXX=C:/MinGW/mingw64/bin/g++.exe

cmake .. -G "MinGW Makefiles" -DCMAKE_BUILD_TYPE=Release -DBUILD_SHARED_LIBS=OFF -DCMAKE_INSTALL_PREFIX=install -DOPENCV_ENABLE_NONFREE=ON -DOPENCV_EXTRA_MODULES_PATH=../opencv_contrib/modules

cmake .. -G "MinGW Makefiles" 
-DCMAKE_BUILD_TYPE=Release 
-DBUILD_SHARED_LIBS=OFF
-DCMAKE_INSTALL_PREFIX=install 
-DOPENCV_ENABLE_NONFREE=ON 
-DBUILD_JAVA=OFF 
-DBUILD_opencv_python3=OFF 
-DCMAKE_INSTALL_PREFIX=install 

////////////////////////////////////////////////////////////////////////////
// worked.    valid *.a files but project still not working 
////////////////////////////////////////////////////////////////////////////

// IMPORTARNT : SET COMPILER TO MINGWIN TO AVOID *.LIB FILES INSTEADD OF EXPECTED *.A

git clone https://github.com/opencv/opencv.git
git clone https://github.com/opencv/opencv_contrib.git

md build
cd build

set CC=C:/MinGW/mingw64/bin/gcc.exe
set CXX=C:/MinGW/mingw64/bin/g++.exe

cmake .. -G "MinGW Makefiles"      ^
-DCMAKE_BUILD_TYPE=Release         ^
-DBUILD_SHARED_LIBS=OFF            ^
-DCMAKE_INSTALL_PREFIX=install     ^
-DOPENCV_ENABLE_NONFREE=ON         ^
-DBUILD_JAVA=OFF                   ^
-DBUILD_opencv_python3=OFF         ^
-DBUILD_opencv_videoio=OFF         ^
-DBUILD_opencv_highgui=OFF         ^
-DWITH_FFMPEG=OFF                  ^
-DWITH_GSTREAMER=OFF               ^
-DWITH_OPENEXR=OFF                 ^
-DWITH_JASPER=OFF                  ^
-DWITH_WEBP=OFF                    ^
-DWITH_TIFF=OFF                    ^
-DWITH_PNG=OFF                     ^
-DWITH_JPEG=OFF                    ^
-DWITH_OPENJPEG=OFF                ^
-DCMAKE_EXE_LINKER_FLAGS="-static" ^
-DCMAKE_SHARED_LINKER_FLAGS="-static" 

 mingw32-make -j8

 mingw32-make install


////////////////////////////////////////////////////////////////////////////
// buscar cmake valido -->    -DWITH_CUDA=ON / OFF
////////////////////////////////////////////////////////////////////////////

git clone https://github.com/opencv/opencv.git
git clone https://github.com/opencv/opencv_contrib.git

md build
cd build


set CC=C:/MinGW/mingw64/bin/gcc.exe
set CXX=C:/MinGW/mingw64/bin/g++.exe

cmake .. -G "MinGW Makefiles" ^
    -DCMAKE_BUILD_TYPE=Release ^
    -DBUILD_SHARED_LIBS=OFF ^
    -DWITH_CUDA=OFF ^
    -DCMAKE_INSTALL_PREFIX=./install ^
    -DBUILD_TESTS=OFF ^
    -DBUILD_PERF_TESTS=OFF ^
    -DENABLE_CXX11=ON ^
    -DWITH_OPENGL=ON ^
    -DWITH_IPP=OFF ^
    -DWITH_DIRECTX=OFF ^
    -DWITH_MSMF=OFF ^
    -DWITH_WINRT=ON ^
    -DWITH_TBB=OFF ^
    -DWITH_OPENMP=ON

 mingw32-make -j8
 mingw32-make install

////////////////////////////////////////////////////////////////////////////
// VCPCKG
////////////////////////////////////////////////////////////////////////////

1) toochain -> mingw64

2) vcpkg install opencv4:x64-mingw-static

3) Not working. errors

file:///C:/Users/pablo.perez/dev/cpp/_install/vcpkg/buildtrees/protobuf/install-x64-mingw-static-rel-out.log

 
*/

/////////////////////////////////////////////////////////
// MSYS UCTR64
/////////////////////////////////////////////////////////

/*
// change to directory
cd /c/Users/pablo.perez/dev/cpp/CPP_GCC_OPEN_CV/mingwin
ls

// ok
g++ -shared -o OpenCvDll.dll OpenCvDll.cpp OpenCvDll.def $(pkg-config --cflags --static --libs opencv4)

// ok
pkg-config --cflags --libs opencv4

// ok (DINAMICO)

g++ -shared -o OpenCvDll.dll OpenCvDll.cpp OpenCvDll.def -IC:/msys64/ucrt64/include/opencv4   $(pkg-config --cflags --static --libs opencv4)

// (ok) - (ESTATICO))

g++ -shared -o OpenCvDll.dll OpenCvDll.cpp OpenCvDll.def -IC:/msys64/ucrt64/include/opencv4  $(pkg-config --cflags --static --libs opencv4)

// ok

$ pkg-config --cflags --static --libs opencv4

-IC:/msys64/ucrt64/include/opencv4 

lopencv_gapi
lopencv_stitching 
lopencv_alphamat 
lopencv_aruco
lopencv_bgsegm 
lopencv_ccalib 
lopencv_cvv 
lopencv_dnn_objdetect 
lopencv_dnn_superres 
lopencv_dpm 
lopencv_face 
lopencv_freetype 
lopencv_fuzzy 
lopencv_hdf 
lopencv_hfs 
lopencv_img_hash -lopencv_intensity_transform 
lopencv_line_descriptor 
lopencv_mcc 
lopencv_ovis 
lopencv_quality 
lopencv_rapid 
lopencv_reg 
lopencv_rgbd 
lopencv_saliency 
lopencv_sfm 
lopencv_signal 
lopencv_stereo 
lopencv_structured_light 
lopencv_phase_unwrapping 
lopencv_superres 
lopencv_optflow 
lopencv_surface_matching 
lopencv_tracking 
lopencv_highgui 
lopencv_datasets 
lopencv_text 
lopencv_plot 
lopencv_videostab 
lopencv_videoio 
lopencv_viz 
lopencv_wechat_qrcode 
lopencv_xfeatures2d 
lopencv_shape
lopencv_ml 
lopencv_ximgproc 
lopencv_video 
lopencv_xobjdetect 
lopencv_objdetect 
lopencv_calib3d
lopencv_imgcodecs 
lopencv_features2d 
lopencv_dnn 
lopencv_flann 
lopencv_xphoto 
lopencv_photo 
lopencv_imgproc 
lopencv_core 
lpthread

g++ -shared -o OpenCvDll.dll OpenCvDll.cpp OpenCvDll.def -IC:/msys64/ucrt64/include/opencv4  <LIBRARIES HERE> $(pkg-config --cflags --static --libs opencv4)

///////////////////////////////////////////////////////////////////////////////////////////

// [PARTIAL REFERENCIES] compkla ok -> prueba exe local ok -> deploy fails

g++ -shared -o OpenCvDll.dll OpenCvDll.cpp OpenCvDll.def \
    -I/mingw64/include/opencv4 -lpthread \
    $(pkg-config --cflags --static --libs opencv4) \
    -lopencv_gapi -lz -llibjpeg -llibpng -lopengl32 -lglu32 -lgdi32 -luser32 -lole32

// [with all referencies : compla ok -> prueba exe local ok -> deploy fails

g++ -shared -o OpenCvDll.dll OpenCvDll.cpp OpenCvDll.def \
    -I/mingw64/include/opencv4 -lpthread \
    $(pkg-config --cflags --static  --libs opencv4) \
    -lopencv_gapi -lz -llibjpeg -llibpng -lopengl32 -lglu32 -lgdi32 -luser32 -lole32 -lopencv_ccalib -lopencv_cvv   -lopencv_dnn_objdetect -lopencv_dnn_superres -lopencv_stitching  -lopencv_alphamat  -lopencv_aruco -lopencv_bgsegm  -lopencv_dpm -lopencv_face -lopencv_freetype -lopencv_fuzzy -lopencv_hdf -lopencv_hfs -lopencv_img_hash -lopencv_intensity_transform -lopencv_line_descriptor -lopencv_mcc -lopencv_ovis -lopencv_quality -lopencv_rapid -lopencv_reg -lopencv_rgbd -lopencv_saliency -lopencv_sfm -lopencv_signal -lopencv_stereo -lopencv_structured_light -lopencv_phase_unwrapping -lopencv_superres -lopencv_optflow -lopencv_surface_matching -lopencv_tracking -lopencv_highgui -lopencv_datasets -lopencv_text -lopencv_plot -lopencv_videostab -lopencv_videoio -lopencv_viz -lopencv_wechat_qrcode -lopencv_xfeatures2d -lopencv_shape -lopencv_ml -lopencv_ximgproc -lopencv_video -lopencv_xobjdetect -lopencv_objdetect -lopencv_calib3d -lopencv_imgcodecs -lopencv_features2d -lopencv_dnn -lopencv_flann -lopencv_xphoto -lopencv_photo -lopencv_imgproc -lopencv_core -lpthread   

//  [with all referencies : compla ok -> prueba exe local ok -> deploy fails

g++ -shared -static-libgcc -static-libstdc++ -m64 -o OpenCvDll.dll OpenCvDll.cpp OpenCvDll.def \
    -I/mingw64/include/opencv4 -lpthread \
    $(pkg-config --cflags --static  --libs opencv4) \
    -lopencv_gapi -lz -llibjpeg -llibpng -lopengl32 -lglu32 -lgdi32 -luser32 -lole32 -lopencv_ccalib -lopencv_cvv   -lopencv_dnn_objdetect -lopencv_dnn_superres -lopencv_stitching  -lopencv_alphamat  -lopencv_aruco -lopencv_bgsegm  -lopencv_dpm -lopencv_face -lopencv_freetype -lopencv_fuzzy -lopencv_hdf -lopencv_hfs -lopencv_img_hash -lopencv_intensity_transform -lopencv_line_descriptor -lopencv_mcc -lopencv_ovis -lopencv_quality -lopencv_rapid -lopencv_reg -lopencv_rgbd -lopencv_saliency -lopencv_sfm -lopencv_signal -lopencv_stereo -lopencv_structured_light -lopencv_phase_unwrapping -lopencv_superres -lopencv_optflow -lopencv_surface_matching -lopencv_tracking -lopencv_highgui -lopencv_datasets -lopencv_text -lopencv_plot -lopencv_videostab -lopencv_videoio -lopencv_viz -lopencv_wechat_qrcode -lopencv_xfeatures2d -lopencv_shape -lopencv_ml -lopencv_ximgproc -lopencv_video -lopencv_xobjdetect -lopencv_objdetect -lopencv_calib3d -lopencv_imgcodecs -lopencv_features2d -lopencv_dnn -lopencv_flann -lopencv_xphoto -lopencv_photo -lopencv_imgproc -lopencv_core -lpthread   

//  [with w1 group] : compla ok -> prueba exe local ok -> deploy fails

>>> TOOLCHAIN MINGW64

cd /c/Users/pablo.perez/dev/cpp/CPP_GCC_OPEN_CV/mingwin

g++ -shared -static-libgcc -static-libstdc++ -m64 -o OpenCvDll.dll OpenCvDll.cpp OpenCvDll.def \
    -I/mingw64/include/opencv4 \
    -Wl,--start-group $(pkg-config --cflags --static  --libs opencv4)  -Wl,--end-group \
    -lopencv_gapi -lz -llibjpeg -llibpng -lopengl32 -lglu32 -lgdi32 -luser32 -lole32 -lopencv_ccalib -lopencv_cvv   -lopencv_dnn_objdetect -lopencv_dnn_superres -lopencv_stitching  -lopencv_alphamat  -lopencv_aruco -lopencv_bgsegm  -lopencv_dpm -lopencv_face -lopencv_freetype -lopencv_fuzzy -lopencv_hdf -lopencv_hfs -lopencv_img_hash -lopencv_intensity_transform -lopencv_line_descriptor -lopencv_mcc -lopencv_ovis -lopencv_quality -lopencv_rapid -lopencv_reg -lopencv_rgbd -lopencv_saliency -lopencv_sfm -lopencv_signal -lopencv_stereo -lopencv_structured_light -lopencv_phase_unwrapping -lopencv_superres -lopencv_optflow -lopencv_surface_matching -lopencv_tracking -lopencv_highgui -lopencv_datasets -lopencv_text -lopencv_plot -lopencv_videostab -lopencv_videoio -lopencv_viz -lopencv_wechat_qrcode -lopencv_xfeatures2d -lopencv_shape -lopencv_ml -lopencv_ximgproc -lopencv_video -lopencv_xobjdetect -lopencv_objdetect -lopencv_calib3d -lopencv_imgcodecs -lopencv_features2d -lopencv_dnn -lopencv_flann -lopencv_xphoto -lopencv_photo -lopencv_imgproc -lopencv_core -lpthread   

COPIAR EN :

/wwwroot/
    ├── x OpenCvDll.dll
    ├── x libgcc_s_seh-1.dll
    ├── x libstdc++-6.dll
    ├── x libwinpthread-1.dll
    ├── x zlib1.dll
    ├── x libjpeg-9.dll  (8)
    └── x libpng16-16.dll


//  [dynamic] : compla [] -> prueba exe local [] -> deploy []

cd /c/Users/pablo.perez/dev/cpp/CPP_GCC_OPEN_CV/mingwin

g++ -shared  -m64 -o OpenCvDll.dll OpenCvDll.cpp OpenCvDll.def \
    -I/mingw64/include/opencv4 \
    -Wl,--start-group $(pkg-config --cflags   --libs opencv4)  -Wl,--end-group \
    -lopencv_gapi -lz -llibjpeg -llibpng -lopengl32 -lglu32 -lgdi32 -luser32 -lole32 -lopencv_ccalib -lopencv_cvv   -lopencv_dnn_objdetect -lopencv_dnn_superres -lopencv_stitching  -lopencv_alphamat  -lopencv_aruco -lopencv_bgsegm  -lopencv_dpm -lopencv_face -lopencv_freetype -lopencv_fuzzy -lopencv_hdf -lopencv_hfs -lopencv_img_hash -lopencv_intensity_transform -lopencv_line_descriptor -lopencv_mcc -lopencv_ovis -lopencv_quality -lopencv_rapid -lopencv_reg -lopencv_rgbd -lopencv_saliency -lopencv_sfm -lopencv_signal -lopencv_stereo -lopencv_structured_light -lopencv_phase_unwrapping -lopencv_superres -lopencv_optflow -lopencv_surface_matching -lopencv_tracking -lopencv_highgui -lopencv_datasets -lopencv_text -lopencv_plot -lopencv_videostab -lopencv_videoio -lopencv_viz -lopencv_wechat_qrcode -lopencv_xfeatures2d -lopencv_shape -lopencv_ml -lopencv_ximgproc -lopencv_video -lopencv_xobjdetect -lopencv_objdetect -lopencv_calib3d -lopencv_imgcodecs -lopencv_features2d -lopencv_dnn -lopencv_flann -lopencv_xphoto -lopencv_photo -lopencv_imgproc -lopencv_core -lpthread   

//  [static / no dependencies] : compla [] -> prueba exe local [] -> deploy []

cd /c/Users/pablo.perez/dev/cpp/CPP_GCC_OPEN_CV/mingwin


g++ -shared -static-libgcc -static-libstdc++ -m64 -o OpenCvDll.dll OpenCvDll.cpp OpenCvDll.def \
    -I/mingw64/include/opencv4 \
    -Wl,--start-group $(pkg-config --cflags --static  --libs opencv4)  -Wl,--end-group \

    "No se encuentra el punto de entrada del procedimiento webpmalloc en la biblioteca c:\msys64\mingw64\bin\libwebpmux-3.dll" 

    libwebpmux, libwebp, libwebpdemux,
 
// static compilation with empty source code
pacman -S mingw-w64-x86_64-opencv

g++ -shared -static-libgcc -static-libstdc++ -m64 -o OpenCvDll.dll OpenCvDll.cpp OpenCvDll.def \
    -I/mingw64/include/opencv2 \
        -Wl,--start-group $(pkg-config --cflags --static  --libs opencv4)  -Wl,--end-group \
           -lopencv_gapi -lz -llibjpeg -llibpng -lopengl32 -lglu32 -lgdi32 -luser32 -lole32 -lopencv_ccalib -lopencv_cvv   -lopencv_dnn_objdetect -lopencv_dnn_superres -lopencv_stitching  -lopencv_alphamat  -lopencv_aruco -lopencv_bgsegm  -lopencv_dpm -lopencv_face -lopencv_freetype -lopencv_fuzzy -lopencv_hdf -lopencv_hfs -lopencv_img_hash -lopencv_intensity_transform -lopencv_line_descriptor -lopencv_mcc -lopencv_ovis -lopencv_quality -lopencv_rapid -lopencv_reg -lopencv_rgbd -lopencv_saliency -lopencv_sfm -lopencv_signal -lopencv_stereo -lopencv_structured_light -lopencv_phase_unwrapping -lopencv_superres -lopencv_optflow -lopencv_surface_matching -lopencv_tracking -lopencv_highgui -lopencv_datasets -lopencv_text -lopencv_plot -lopencv_videostab -lopencv_videoio -lopencv_viz -lopencv_wechat_qrcode -lopencv_xfeatures2d -lopencv_shape -lopencv_ml -lopencv_ximgproc -lopencv_video -lopencv_xobjdetect -lopencv_objdetect -lopencv_calib3d -lopencv_imgcodecs -lopencv_features2d -lopencv_dnn -lopencv_flann -lopencv_xphoto -lopencv_photo -lopencv_imgproc -lopencv_core -lpthread   

g++ -shared -static-libgcc -static-libstdc++ -m64 -o OpenCvDll.dll OpenCvDll.cpp OpenCvDll.def \
     -I/mingw64/include/opencv4 -L/mingw64/lib           

g++ -shared -static-libgcc -static-libstdc++ -m64 -o OpenCvDll.dll OpenCvDll.cpp OpenCvDll.def \
     -I/mingw64/include/opencv4 -L/mingw64/lib  -lopencv_core   -lopencv_imgcodecs  -lopencv_gapi  -lz -llibjpeg -llibpng -lopengl32 -lglu32 -lgdi32 -luser32 -lole32   


          ltbb
   

 
     "No se encuentra el punto de entrada del procedimiento webpmalloc en la biblioteca c:\msys64\mingw64\bin\libwebpmux-3.dll" 
            -
    pacman -S mingw-w64-x86_64-libwebp


    cd /c/msys64/mingw64/

    */

#include <opencv2/opencv.hpp>
#include <vector>
#include <string>
#include <iostream>


#define DLL_EXPORT extern "C" __declspec(dllexport) __stdcall 

using namespace std;


std::vector<std::string> detectShapes(const cv::Mat& inputImage) {
    std::vector<std::string> shapes;

    // Ensure the input image is valid
    if (inputImage.empty()) {
        std::cerr << "Error: Input image is empty!" << std::endl;
        return shapes;
    }

    // Create matrices for grayscale and edges
    cv::Mat gray, edges;

    // Convert the input image to grayscale
    cv::cvtColor(inputImage, gray, cv::COLOR_BGR2GRAY);

    // Apply Canny edge detector
    cv::Canny(gray, edges, 50, 150, 3, false);

    // Find contours
    std::vector<std::vector<cv::Point>> contours;
    std::vector<cv::Vec4i> hierarchy;
    cv::findContours(edges, contours, hierarchy, cv::RETR_EXTERNAL, cv::CHAIN_APPROX_SIMPLE);

    // Iterate over contours and classify shapes
    for (size_t i = 0; i < contours.size(); ++i) {
        std::vector<cv::Point> contour = contours[i];
        std::vector<cv::Point> approx;

        // Approximate the contour to a polygon
        double epsilon = 0.04 * cv::arcLength(contour, true);
        cv::approxPolyDP(contour, approx, epsilon, true);

        std::string shape;
        if (approx.size() == 3) {
            shape = "Triángulo";
        } else if (approx.size() == 4) {
            // Calculate bounding rectangle and aspect ratio
            cv::Rect rect = cv::boundingRect(approx);
            double aspectRatio = static_cast<double>(rect.width) / rect.height;
            shape = (aspectRatio >= 0.95 && aspectRatio <= 1.05) ? "Cuadrado" : "Rectángulo";
        } else if (approx.size() > 4) {
            shape = "Círculo";
        }

        if (!shape.empty()) {
            shapes.push_back(shape);
        }
    }

    // Return the detected shapes
    return shapes;
}


DLL_EXPORT const char* OpenCvReadImage()
{
    const char * result = "OK";
 
    cv::Mat image = cv::imread("image.png");
    
    if (image.empty()) {
        std::cerr << "Could not read the image!" << std::endl;
        return "Could not read the image 'image.png'";
    }

    // Detect shapes in the image
    std::vector<std::string> shapes = detectShapes(image);

    // Print the detected shapes
    std::cout << "Detected shapes:" << std::endl;
    for (const std::string& shape : shapes) {
        //std::cout << "- " << shape << std::endl;
        result = shape.c_str();
    }

    return result;
}



