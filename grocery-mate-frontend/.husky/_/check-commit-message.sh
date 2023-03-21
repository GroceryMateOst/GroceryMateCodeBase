#!/bin/sh

commit_msg="$(cat $1)"
echo $commit_msg
regex=".*\[#([0-9]+)\]$"

if ! [[ $commit_msg =~ $regex ]]; then
  echo "ERROR: Commit message does not include issue ID at the end with a number enclosed in square brackets (e.g. [#10])"
  exit 1
fi