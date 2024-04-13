project('__APP_NAME__',
	version: '0.1.0',
	meson_version: '>= 0.63.0',
	default_options: [ 'warning_level=2', 'werror=false', ],
)

app_id = '__APP_ID__'

i18n = import('i18n')
gnome = import('gnome')
prefix = get_option('prefix') # should be /usr
bindir = get_option('bindir') # should be bin
datadir = get_option('datadir') # should be /usr/share
pkgdatadir = join_paths(prefix, datadir, meson.project_name())
moduledir = join_paths(pkgdatadir, meson.project_name())
localedir = join_paths(prefix, get_option('localedir'))

subdir('__APP_NAME__')
subdir('data')
subdir('po')

gnome.post_install(
     glib_compile_schemas: true,
    gtk_update_icon_cache: true,
  update_desktop_database: true,
)