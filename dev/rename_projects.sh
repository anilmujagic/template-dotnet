#!/bin/bash

set -e

pushd "${0%/*}" # Go to script directory

cd ..

#old_name=$(basename $(find ./src -name *.sln) '.sln')
old_name="$1"
new_name="$2"
echo "Old name: $old_name"
echo "New name: $new_name"

# Derive DB name
old_db_name=$(echo $old_name | sed -E 's/([A-Z])/_\1/g' | sed -E 's/^_//' | tr '[:upper:]' '[:lower:]')
new_db_name=$(echo $new_name | sed -E 's/([A-Z])/_\1/g' | sed -E 's/^_//' | tr '[:upper:]' '[:lower:]')
echo "Old DB name: $old_db_name"
echo "New DB name: $new_db_name"

# Rename directories
echo 'Renaming directories...'
find . -name *${old_name}* -type d -not -path '*/\.*' | while read old_path; do
    new_path=$(echo $old_path | sed "s/$old_name/$new_name/")
    if [ "$new_path" != "$old_path" ]; then
        #echo "Renaming directory $old_path to $new_path"
        mv "$old_path" "$new_path"
    fi
done

# Rename files
echo 'Renaming files...'
find . -name *${old_name}* -type f -not -path '*/\.*' | while read old_path; do
    new_path=$(echo $old_path | sed "s/$old_name/$new_name/")
    if [ "$new_path" != "$old_path" ]; then
        #echo "Renaming file $old_path to $new_path"
        mv "$old_path" "$new_path"
    fi
done

# Rename code
echo 'Renaming code...'
grep -rl "$old_name" --include=*.{cs,csproj,sln,fsx,sh} --exclude-dir={.git,.idea,.vs,.vscode} . | while read file; do
    #echo "Renaming code in $file"
    sed -i -- "s/$old_name/$new_name/g" "$file"
    rm "${file}--"
done

# Rename DB
echo 'Renaming DB...'
grep -rl "$old_db_name" --include=*.{sql,fsx,json} --exclude-dir={.git,.idea,.vs,.vscode} . | while read file; do
    #echo "Renaming DB in $file"
    sed -i -- "s/$old_db_name/$new_db_name/g" "$file"
    rm "${file}--"
done

popd # Go back to caller directory
