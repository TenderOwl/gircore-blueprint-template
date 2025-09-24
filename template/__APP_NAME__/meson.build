
dotnet_bin  = find_program('dotnet')

dotnet_options = ['--nologo', '-r=linux-x64']

if get_option('buildtype') == 'release'
  dotnet_target = 'buildtype'
else
  dotnet_options += [ '-c=Debug' ]
  dotnet_target = 'Debug'
endif
