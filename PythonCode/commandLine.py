# -*- coding: utf-8 -*-
import argparse
import getpass
import os
import subprocess
import configparser

from loguru import logger

config = configparser.ConfigParser()
config.read('config.ini', encoding='utf-8')


def execute_command(command_str):
    try:
        # netbios_info_command=['nbtscan.exe', '-s :', '172.2.6.0/24']
        # out_bytes = subprocess.check_output(netbios_info_command)
        logger.info(f'执行命令{command_str}')
        out_bytes = subprocess.check_output(command_str, shell=True)
        out_text = out_bytes.decode('gbk')
        for index, line in enumerate(out_text.splitlines()):
            items = [item.strip() for item in line.split(':')]
            print(f'第{index + 1}行', items[0], items[1], items[-1])
    except subprocess.CalledProcessError as e:
        out_bytes = e.output  # Output generated before error
        code = e.returncode  # Return code


veracrypt_home = config.get('veracrypt', 'BinHome')


def create_disk(filename='test.disk', size='10M', password='test', verbose=False):
    """
    //"VeraCrypt Format.exe" /create test.hc /password test /hash sha-512 /encryption serpent /filesystem NTFS
         /size 100M /force /silent
    :return:
    """
    logger.debug(f'执行创建 加密盘文件名：{filename} 大小：{size} 密码：{password}')
    command_str = f'{os.getcwd()}/{veracrypt_home}/VeraCryptFormat.exe|/create {filename}|/size {size}|/password {password} |/encryption serpent|/hash sha-512' \
                  f'|/filesystem NTFS|/force'
    # 不显示提示消息,静默输出
    if not verbose:
        command_str += '|/silent'
    text = command_str.replace('|', ' ')
    execute_command(text)


def mount_disk(filename='test.disk', password='test', letter='o', verbose=False):
    logger.debug(f'挂载加密盘{filename},盘符为{letter}，密码{password}')
    command_str = f'{os.getcwd()}/{veracrypt_home}/VeraCrypt.exe|/volume {filename}|/letter {letter}|/password {password}' \
                  f'|/quit'
    if not verbose:
        command_str += '|/silent'
    text = command_str.replace('|', ' ')
    execute_command(text)


def unmount_disk(letter='o', verbose=False):
    logger.debug(f'卸载盘符{letter}')
    command_str = f'{os.getcwd()}/{veracrypt_home}/VeraCrypt.exe|/dismount {letter}|/force' \
                  f'|/quit'
    if not verbose:
        command_str += '|/silent'
    text = command_str.replace('|', ' ')
    execute_command(text)


def parser_command():
    parser = argparse.ArgumentParser(description='Python 封装veracrypt.exe命令行程序')
    parser.add_argument('command', metavar='command <mount|umount|create>', choices=['mount', 'umount', 'create'],
                        help="选项")
    parser.add_argument('-f', '--filename', default="test.disk", help="需要挂载的磁盘文件")
    parser.add_argument('-s', '--size', default='10M')  # 步骤二，后面的help是我的描述
    parser.add_argument('-p', '--password')  # 步骤二，后面的help是我的描述
    parser.add_argument('-d', '--device', default='o', help='需要挂载的磁盘驱动器盘符')  # 步骤二，后面的help是我的描述
    parser.add_argument('-v', '--verbose', action='store_true', help="显示提示框")
    parser.add_argument('-mount', '--mount', action='store_true')

    args = parser.parse_args()
    filename = args.filename
    size = args.size
    password = args.password
    device = args.device
    verbose = args.verbose

    logger.debug(f'verbose 显示{args.verbose}')
    if args.command == 'create':
        if password is None:
            password = getpass.getpass("请输入用户密码：")
        create_disk(filename, size, password, verbose)
    if args.command == 'mount':
        if password is None:
            password = getpass.getpass("请输入用户密码：")
        mount_disk(filename, password, device, verbose)
    if args.command == 'umount':
        unmount_disk(device, verbose)


if __name__ == '__main__':
    # # create_disk(filename='test.disk', size='4M')
    # unmount_disk()
    # mount_disk(letter='A')
    #
    # mount -
    parser_command()
