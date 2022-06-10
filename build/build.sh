#!/bin/bash

# Prevent packing of resource fork files on macOS (./._xxxxx)
export COPYFILE_DISABLE=true

set -e

pushd "${0%/*}" # Go to script directory
build_dir="$(pwd)"
temp_dir="$build_dir/temp"
output_dir="$build_dir/output"

rm -rf "$temp_dir"
mkdir -p -m 777 "$temp_dir"

rm -rf "$output_dir"
mkdir -p "$output_dir"

####################################################################################################
# Application
####################################################################################################
project_names="
    MyApp.Api
"

for project_name in $project_names; do
    echo "--- Building project: $project_name"

    project_source_dir="../src/$project_name"
    project_temp_dir="$temp_dir/$project_name"

    pushd "$project_source_dir"
    mkdir -p "$project_temp_dir"
    dotnet publish -c Release -o "$project_temp_dir"
    popd

    pushd "$project_temp_dir"
    mv appsettings.json appsettings.json.local
    zip "$output_dir/$project_name.zip" -rq * -x 'appsettings.json' -x '*.pdb'
    popd
done

####################################################################################################
# Database
####################################################################################################
pushd "../db"
zip "$output_dir/db.zip" -rq *
popd

####################################################################################################
# Finalize
####################################################################################################
rm -rf "$temp_dir" # Clean up
popd # Go back to caller directory

# Show build output
echo "--- Build output in $output_dir"
ls -lh "$output_dir"
